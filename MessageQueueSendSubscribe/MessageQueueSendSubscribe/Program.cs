using System;
using Azure.Messaging.ServiceBus;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageQueueSendSubscribe
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
    class Program
    {

         const string subscriptionName = "QueueServiceSubscriptionNVR";
        //const string QueueName = "servicequeue";
        const string connectionString = "Endpoint=sb://queueservicenvr.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ooIafHiGNutAoDp+2Tl9ZqEPbZY43ZeSl6Q5ZB2pBx8=";
        const string topicName = "servicetopic";
        static async Task Main()
        {

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
                LastName = "Zupan",
                MarketName = "Chicago",
                MoveDate = null,
                Phone = null,
                PriceRange = null,
                Source = "HOMES",
                State = "",
                Type = "Manually Entered Lead",
                Zip = "20910"

            };

             await SendMessageToTopicAsync();

            //string data = JsonConvert.SerializeObject(newLead);
            //await SendJsonToTopicAsyn(data);

        }

        static async Task SendJsonToTopicAsyn(string message)
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the topic
                ServiceBusSender sender = client.CreateSender(topicName);
                //Console.WriteLine("Write a message to send to NVR message Queue: ");
                //string message = Console.ReadLine();
                await sender.SendMessageAsync(new ServiceBusMessage(message));
                Console.WriteLine($"Sent a Json message:" + message + " to the topic: {subscriptionName}");
            }
        }

        static async Task SendMessageToTopicAsync()
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the topic
                ServiceBusSender sender = client.CreateSender(topicName);
                Console.WriteLine("Write a message to send to NVR Subscription Queue: ");
                string message = Console.ReadLine();
                await sender.SendMessageAsync(new ServiceBusMessage(message));
                Console.WriteLine($"Sent a single message to the topic: {subscriptionName}");
                string message2 = Console.ReadLine();
                await sender.SendMessageAsync(new ServiceBusMessage(message2));
                Console.WriteLine($"Sent a single message to the topic: {subscriptionName}");
                Console.ReadKey();
            }
        }
    }
}
