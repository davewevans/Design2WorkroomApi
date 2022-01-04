using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Design2WorkroomApi.DTOs
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }

        public Guid? WorkroomId { get; set; }

        public string ToEmailAddress { get; set; }

        public string FromEmailAddress { get; set; }

        public string Subject { get; set; }

        public string TextBody { get; set; }

        public string HtmlBody { get; set; }

        public string Tag { get; set; }

        public string MessageStream { get; set; }

        public EmailStatus Status { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime DateReceived { get; set; }

        public Guid DesignerId { get; set; }
    }
}
