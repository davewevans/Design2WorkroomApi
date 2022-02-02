namespace Design2WorkroomApi.DTOs
{
    public class DesignConceptUpdateDto
    {
        public string ImageUrl { get; set; }

        public Guid DesignerId { get; set; }

        public Guid ClientId { get; set; }

        public bool IsArchived { get; set; }

        public bool IsApproved { get; set; }
    }
}
