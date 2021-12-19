using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("Notifications")]
    public class NotificationModel : Entity
    {
        public NotificationModel(string message)
        {
                Message = message;
        }

        public int? ClientId { get; set; }

        public int? WorkroomId { get; set; }

        public string Message { get; set; }

        [ForeignKey(nameof(DesignerModel))]
        public Guid DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;
    }
}
