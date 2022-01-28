using Design2WorkroomApi.Models;
using PostmarkDotNet;
using PostmarkDotNet.Model;

namespace Design2WorkroomApi.Helpers
{

    public class FileAttachment
    {
        public byte[] Bytes { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

        public string Mime { get; set; }

        public string Extension { get; set; }
    }

    public class EmailSender : IPostmarkEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailPostmarkAsync(PostmarkEmailModel email, List<FileAttachment> fileAttachments = null)
        {
            // Send an email asynchronously:
            var message = new PostmarkMessage()
            {
                To = email.ToEmailAddress,
                From = email.FromEmailAddress,
                TrackOpens = true,
                Subject = email.Subject,
                TextBody = email.TextBody,
                HtmlBody = email.HtmlBody,
                MessageStream = string.IsNullOrWhiteSpace(email.MessageStream) ? "outbound" : email.MessageStream,
                Tag = email.Tag,
                Headers = email.Headers
            };

            try
            {
                if (fileAttachments != null && fileAttachments.Count > 0)
                {
                    foreach (var attachment in fileAttachments)
                    {
                        message.AddAttachment(attachment.Bytes, attachment.FileName, attachment.Mime, $"cid:embed_name{attachment.Extension}");
                    }
                }

                string postmarkApiKey = _configuration["PostmarkApiKey"];

                var client = new PostmarkClient(postmarkApiKey);
                var sendResult = await client.SendMessageAsync(message);

                if (sendResult.Status == PostmarkStatus.Success)
                {
                    /* Handle success */
                    // Console.WriteLine("Email sent successfully");
                }
                else
                {
                    /* Resolve issue.*/
                    // Console.WriteLine("Email not sent");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }

        public async Task SendTestEmailPostmark()
        {
            var email = new PostmarkEmailModel
            {
                ToEmailAddress = "davewevans72@gmail.com",
                FromEmailAddress = "info@daveevans.tech",
                Subject = "A complex email",
                TextBody = "Hello dear Postmark user.",
                HtmlBody = "<strong>Hello</strong> dear Postmark user.",
                Tag = "Test email",
                Headers = null // new MailHeader("X-Dave-Evans", value: "Test Header content"),
            };

            try
            {
                await SendEmailPostmarkAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
