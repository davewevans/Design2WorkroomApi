using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Design2WorkroomApi.Models
{
    [Table("Attachments")]
    public class AttachmentsModel : Entity
    {
        [ForeignKey(nameof(EmailModel))]
        public Guid EmailId { get; set; }
        
        public string Name { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
    }
}
