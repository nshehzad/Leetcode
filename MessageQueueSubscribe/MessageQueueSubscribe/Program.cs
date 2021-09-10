using System;
using Azure.Messaging.ServiceBus;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageQueueSubscribe
{
    class WebLead
    {
        public string Address;
        public bool AskedToBeContacted;
        public string BuilderName;
        public string City;
        public string Comments;
        public long CommunityId;
        public string CommunityName;
        public string ContactPreference;
        public string DateCreated;
        public string Email;
        public string FirstName;
        public string IlsEmail;
        public bool IsRealtor;
        public string LastName;
        public string MarketName;
        public string MoveDate;
        public string Phone;
        public string PriceRange;
        public string Source;
        public string State;
        public string Type;
        public string Zip;

    }

    //string json = "{ "Address":null,"AskedToBeContacted":true,"BuilderName":"R","City":null,"Comments":null,"CommunityId":"10222120150778","CommunityName":"Oldham Crossing","ContactPreference":"Phone","DateCreated":"2021-04-15 10:06:20","Email":"fake439874@test.com","FirstName":"Jr.","IlsEmail":"novateam@ryanhomes.com","IsRealtor":false,"LastName":"Zupan","MarketName":"Chicago","MoveDate":null,"Phone":null,"PriceRange":null,"Source":"HOMES","State":"  ","Type":"Manually Entered Lead","Zip":"20910"}"


    class Program
    {
        const string subscriptionName = "QueueServiceSubscriptionNVR";
        const string connectionString = "Endpoint=sb://queueservicenvr.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ooIafHiGNutAoDp+2Tl9ZqEPbZY43ZeSl6Q5ZB2pBx8=";
        const string topicName = "servicetopic";

        static async Task Main()
        {
            //string json = "{ "Address":null,"AskedToBeContacted":true,"BuilderName":"R","City":null,"Comments":null,"CommunityId":"10222120150778","CommunityName":"Oldham Crossing","ContactPreference":"Phone","DateCreated":"2021-04-15 10:06:20","Email":"fake439874@test.com","FirstName":"Jr.","IlsEmail":"novateam@ryanhomes.com","IsRealtor":false,"LastName":"Zupan","MarketName":"Chicago","MoveDate":null,"Phone":null,"PriceRange":null,"Source":"HOMES","State":"  ","Type":"Manually Entered Lead","Zip":"20910"}"

            WebLead newLead = new WebLead
            {
                Address = null,
                AskedToBeContacted = true,
                BuilderName = "R",
                City = null,
                Comments = null,
                CommunityId = 10222120150778,
                CommunityName = "Oldham Crossing",
                ContactPreference = "Phone",
                DateCreated = "2021-04-15 10:06:20",
                FirstName = "Jr.",
                Email = "fake439874@test.com",
                IlsEmail = "novateam@ryanhomes.com",
                IsRealtor = false,
                LastName= "Zupan",
                MarketName = "Chicago",
                MoveDate = null,
                Phone = null,
                PriceRange = null,
                Source = "HOMES",
                State = "",
                Type= "Manually Entered Lead",
                Zip = "20910"

            };

            string data = JsonConvert.SerializeObject(newLead);

            //await SendMessageToTopicAsync(data);

            // send a batch of messages to the topic
            //await SendMessageBatchToTopicAsync();

            // receive messages from the subscription
            await ReceiveMessagesFromSubscriptionAsync();
        }

        static async Task SendMessageToTopicAsync(string Message)
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the topic
                ServiceBusSender sender = client.CreateSender(topicName);
                await sender.SendMessageAsync(new ServiceBusMessage(Message));
                Console.WriteLine($"Sent a single message to the topic: {topicName}");
            }
        }
        static Queue<ServiceBusMessage> CreateMessages()
        {
            // create a queue containing the messages and return it to the caller
            Queue<ServiceBusMessage> messages = new Queue<ServiceBusMessage>();
            messages.Enqueue(new ServiceBusMessage("First message"));
            messages.Enqueue(new ServiceBusMessage("Second message"));
            messages.Enqueue(new ServiceBusMessage("Third message"));
            return messages;
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received message: {body} from subscription: {subscriptionName}");

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        static async Task SendMessageBatchToTopicAsync()
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {

                // create a sender for the topic 
                ServiceBusSender sender = client.CreateSender(topicName);

                // get the messages to be sent to the Service Bus topic
                Queue<ServiceBusMessage> messages = CreateMessages();

                // total number of messages to be sent to the Service Bus topic
                int messageCount = messages.Count;

                // while all messages are not sent to the Service Bus topic
                while (messages.Count > 0)
                {
                    // start a new batch 
                    using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

                    // add the first message to the batch
                    if (messageBatch.TryAddMessage(messages.Peek()))
                    {
                        // dequeue the message from the .NET queue once the message is added to the batch
                        messages.Dequeue();
                    }
                    else
                    {
                        // if the first message can't fit, then it is too large for the batch
                        throw new Exception($"Message {messageCount - messages.Count} is too large and cannot be sent.");
                    }

                    // add as many messages as possible to the current batch
                    while (messages.Count > 0 && messageBatch.TryAddMessage(messages.Peek()))
                    {
                        // dequeue the message from the .NET queue as it has been added to the batch
                        messages.Dequeue();
                    }

                    // now, send the batch
                    await sender.SendMessagesAsync(messageBatch);

                    // if there are any remaining messages in the .NET queue, the while loop repeats 
                }

                Console.WriteLine($"Sent a batch of {messageCount} messages to the topic: {topicName}");
            }
        }

        static async Task ReceiveMessagesFromSubscriptionAsync()
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                //create a processor that we can use to process the messages
                ServiceBusProcessor processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());

                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                //add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                //start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Listening to NVR subscription Queue... Press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
        }
    }
}
