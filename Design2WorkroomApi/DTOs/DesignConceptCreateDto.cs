﻿namespace Design2WorkroomApi.DTOs
{
    public class DesignConceptCreateDto
    {
        public string ImageUrl { get; set; }

        public Guid DesignerId { get; set; }

        public Guid ClientId { get; set; }
    }
}
