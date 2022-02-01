namespace Design2WorkroomApi.DTOs
{
    public class WorkOrderItemCreateDto
    {
        public string Item { get; set; }

        public string? Description { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public string Color { get; set; }

        public string Fabric { get; set; }

        public Guid? WorkOrderId { get; set; }
    }
}
