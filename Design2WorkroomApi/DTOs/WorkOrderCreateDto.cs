﻿namespace Design2WorkroomApi.DTOs
{
    public class WorkOrderCreateDto
    {
        public int WorkOrderNumber { get; set; }

        public DateTime DateOrdered { get; set; }

        public Guid? WorkroomId { get; set; }

        public Guid? ClientId { get; set; }

        public Guid DesignerId { get; set; }

        public List<WorkOrderItemDto> WorkOrderItems { get; set; } = null!;
    }
}