using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Azure.Messaging.ServiceBus;
 

namespace MessageQueue
{
    class Program
    {
        static void Main(string[] args)
        {
 
            //AzureQueue queue = new AzureQueue();
            //Task.Run(async () => await AzureQueue.SendMessagesToQueue());

            var data = AzureQueue.SendMessagesToQueue();
            //implicitly waits for the result and makes synchronous call. 
            //no need for Console.ReadKey()
            //Console.Write(data.Result); Console.Write(data.Result);
            //synchronous call .. same as previous one
            //Console.Write(AzureQueue.SendMessagesToQueue());
            //AzureQueue.SendMessagesToQueue().GetAwaiter().OnCompleted(() => {
            //    Console.Write("done");
            //});
            //LinkedListNode<string> node = new LinkedListNode<string>();
            
            
        }

    }


    static class AzureQueue
    {

        public static async Task SendMessagesToQueue()
        {
            // add the messages that we plan to send to a local queue
            Queue<ServiceBusMessage> messages = new Queue<ServiceBusMessage>();
            messages.Enqueue(new ServiceBusMessage("First message"));
            messages.Enqueue(new ServiceBusMessage("Second message"));
            messages.Enqueue(new ServiceBusMessage("Third message"));

            // create a message batch that we can send
            // total number of messages to be sent to the Service Bus queue
            int messageCount = messages.Count;
            string connectionString = "Endpoint=sb://queueservicenvr.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ooIafHiGNutAoDp+2Tl9ZqEPbZY43ZeSl6Q5ZB2pBx8=";

            //string connectionString = "<connection_string>";
            //QueueServiceNVR / servicequeue
            string queueName = "servicequeue";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);
            // while all messages are not sent to the Service Bus queue
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
           
        }

        public static async Task ReceiveMessageFromQueue(string message)
        {

            string connectionString = "Endpoint=sb://queueservicenvr.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ooIafHiGNutAoDp+2Tl9ZqEPbZY43ZeSl6Q5ZB2pBx8=";
            string queueName = "<queue_name>"; //QueueServicePOC or QueueServiceNVR

            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage busMessage = new ServiceBusMessage(message);

            // send the message
            await sender.SendMessageAsync(busMessage);

            // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // the received message is a different type as it contains some service set properties
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

            // get the message body as a string
            string body = receivedMessage.Body.ToString();
            Console.WriteLine(body);

        }



    }
}
