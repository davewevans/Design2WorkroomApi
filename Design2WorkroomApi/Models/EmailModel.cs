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

        public DateTime? DateSent { get; set; }

        public DateTime? DateReceived { get; set; }

        [ForeignKey(nameof(DesignerModel))]
        public Guid? DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;
    }
}
