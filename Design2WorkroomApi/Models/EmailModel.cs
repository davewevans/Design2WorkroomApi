using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("Emails")]
    public class EmailModel : Entity
    {
        public EmailModel(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }

        public int? ClientId { get; set; }

        public int? WorkroomId { get; set; }

        public string Subject { get; set; } 

        public string Body { get; set; } 

        public EmailStatus Status { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime DateReceived { get; set; }

        [ForeignKey(nameof(DesignerModel))]
        public Guid DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;
    }
}
