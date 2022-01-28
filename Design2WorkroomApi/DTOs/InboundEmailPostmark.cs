namespace Design2WorkroomApi.DTOs
{
    public class InboundEmailPostmark
    {
        public string From { get; set; }
        public ToEmailFull ToFull { get; set; }
        public string Subject { get; set; }
        //public DateTime Date { get; set; }
        public string HtmlBody { get; set; }
        public AttachmentsArray[] Attachments { get; set; }
    }

    public class ToEmailFull
    {
        public string Email { get; set; }
    }

    public class AttachmentsArray
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
    }
}
