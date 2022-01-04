using PostmarkDotNet.Model;

namespace Design2WorkroomApi.Models
{
    public class PostmarkEmailModel
    {
        public string ToEmailAddress { get; set; }

        public string FromEmailAddress { get; set; }

        public string Subject { get; set; }

        public string TextBody { get; set; }

        public string HtmlBody { get; set; }

        public string Tag { get; set; }

        public string MessageStream { get; set; }

        public HeaderCollection Headers { get; set; }

        public List<string> ImageFileAttachments { get; set; }
    }
}
