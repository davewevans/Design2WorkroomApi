namespace Design2WorkroomApi.DTOs
{
    public class DesignConceptsApprovalsCreateDto
    {
        public Guid ClientId { get; set; }

        public bool IsApproved { get; set; }

        public Guid DesignConceptId { get; set; }
    }
}
