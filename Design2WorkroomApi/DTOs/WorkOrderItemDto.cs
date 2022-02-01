using Design2WorkroomApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Design2WorkroomApi.DTOs
{
    public class WorkOrderItemDto
    {
        public Guid Id { get; init; }

        public string Item { get; set; }

        public string? Description { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public string Color { get; set; }

        public string Fabric { get; set; }

        public Guid? WorkOrderId { get; set; }
    }
}
