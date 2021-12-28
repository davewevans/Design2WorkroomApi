namespace Design2WorkroomApi.DTOs
{
    public class DesignConceptDto
    {
        public Guid Id { get; init; }

        public string ImageUrl { get; set; }

        public Guid DesignerId { get; set; }

        public Guid ClientId { get; set; }
    }
}
