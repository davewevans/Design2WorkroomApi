using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Design2WorkroomApi.Models
{
    [Table("ClientDesign")]
    public class ClientDesignModel : Entity
    {
        [ForeignKey(nameof(ClientModel))]
        public Guid? ClientId { get; set; }

        [ForeignKey(nameof(DesignerModel))]
        public Guid? DesignerId { get; set; }

        public string ImageUrl { get; set; }

        public bool IsArchived { get; set; }

        public ClientModel client { get; set; } = null;
        public DesignerModel designer { get; set; } = null;
    }
}
