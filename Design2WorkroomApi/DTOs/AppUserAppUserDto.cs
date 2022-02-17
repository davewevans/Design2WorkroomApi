using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.DTOs
{
    public class AppUserAppUserDto
    {
        public Guid AppUserParentId { get; set; }

        public Guid AppUserChildId { get; set; }

        public bool Active { get; set; }
    }
}
