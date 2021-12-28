using System.ComponentModel.DataAnnotations.Schema;

namespace Design2WorkroomApi.Models
{
    [Table("DesignConcepts")]
    public class DesignConceptModel: Entity
    {
        public string ImageUrl { get; set; }

        public Guid DesignerId { get; set; }

        public Guid ClientId { get; set; }
    }
}
