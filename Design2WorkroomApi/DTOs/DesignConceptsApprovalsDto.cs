﻿namespace Design2WorkroomApi.DTOs
{
    public class DesignConceptsApprovalsDto
    {
        public Guid ClientId { get; set; }
        public bool IsApproved { get; set; }
        public Guid DesignConceptId { get; set; }
    }
}
