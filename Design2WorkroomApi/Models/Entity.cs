using System.ComponentModel.DataAnnotations;

namespace Design2WorkroomApi.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
