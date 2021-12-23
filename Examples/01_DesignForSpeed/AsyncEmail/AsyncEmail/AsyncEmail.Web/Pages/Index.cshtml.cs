using AsyncEmail.Emailer;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AsyncEmail.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Emailer.Emailer _emailer;

        public IndexModel(ILogger<IndexModel> logger, Emailer.Emailer emailer)
        {
            _logger = logger;
            _emailer = emailer;
        }

        public void OnGet()
        {

        }

        public async Task OnPostAsync(string type)
        {
            if (type.Equals("noqueue", StringComparison.OrdinalIgnoreCase))
            {
                for (int x = 0; x < 50; x++)
                {
                    Email email = GenerateEmail();

                    await _emailer.SendEmail(email.Subject, email.Body, email.To, email.From);
                }
            }
            else if (type.Equals("queue", StringComparison.OrdinalIgnoreCase))
            {
                // Let's be honest, this should be in configuration, but this is a demo.
                // Do as I say, not as I do.
                var queueConnectionString = "UseDevelopmentStorage=true";
                var queueName = "incoming-emails";

                // We're using Azure queues
                var emailQueueClient = new QueueClient(queueConnectionString, queueName);
                await emailQueueClient.CreateIfNotExistsAsync();

                for (int x = 0; x < 50; x++)
                {
                    Email email = GenerateEmail();

                    string messageAsJson = JsonConvert.SerializeObject(email);
                    byte[] messageAsBytes = System.Text.Encoding.UTF8.GetBytes(messageAsJson);
                    string messageAsBase64 = Convert.ToBase64String(messageAsBytes);

                    await emailQueueClient.SendMessageAsync(messageAsBase64);
                }
            }
            else
            {
                throw new Exception("What what?");
            }
        }

        private static Email GenerateEmail()
        {
            var avengers = new List<string>() { "stark", "rogers", "romanoff", "banner", "barton", "thor", "fury", "rhodes", "maximoff", "vision", "parker", "rabbit", "wilson", "danvers", "lang", "quill", "drax", "gamora" };

            var email = new Email();
            email.Subject = "Thanos Was Right";

            var randomNumberGenerator = new Random();
            email.Body = LoremNET.Lorem.Paragraph(randomNumberGenerator.Next(1,256), randomNumberGenerator.Next(1,12));
            var rnd = new Random().Next(avengers.Count);
            email.To = $"{avengers[rnd]}@avengers.net";

            rnd = randomNumberGenerator.Next(avengers.Count);
            email.From = $"{avengers[rnd]}@avengers.net";
            return email;
        }
    }
}