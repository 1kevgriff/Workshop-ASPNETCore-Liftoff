// See https://aka.ms/new-console-template for more information
using AsyncEmail.Emailer;
using Azure.Storage.Queues;
using Newtonsoft.Json;

Console.WriteLine("Async Email");

// Let's be honest, this should be in configuration, but this is a demo.
// Do as I say, not as I do.
var queueConnectionString = "UseDevelopmentStorage=true";
var queueName = "incoming-emails";

// We're using Azure queues
var emailQueueClient = new QueueClient(queueConnectionString, queueName);
await emailQueueClient.CreateIfNotExistsAsync();

var emailer = new Emailer();
var resetEvent = new ManualResetEvent(false);
while (!resetEvent.WaitOne(500))
{
    Console.WriteLine("Checking queues");

    // is there anything to process?
    var message = await emailQueueClient.ReceiveMessageAsync();

    if (message.Value != null)
    {
        try
        {
            var base64EncodedBytes = System.Convert.FromBase64String(message.Value.Body.ToString());
            var messageAsJson = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var messageAsEmail = JsonConvert.DeserializeObject<Email>(messageAsJson);

            if (messageAsEmail == null) throw new Exception("Message was null");

            await emailer.SendEmail(messageAsEmail.Subject, messageAsEmail.Body, messageAsEmail.To, messageAsEmail.From);

            // done!  cleanup
            await emailQueueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
        } catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

}