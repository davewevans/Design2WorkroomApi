using System;

namespace Design2WorkroomApi.DTOs
{
    public class DesignConceptUpdate
    {
        public string ImageUrl { get; set; }

        public Guid DesignerId { get; set; }

        public Guid ClientId { get; set; }

        public bool IsArchived { get; set; }
    }
}
