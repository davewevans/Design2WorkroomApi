namespace Design2WorkroomApi.Models
{
    public class B2CUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class B2CUserResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string B2cObjectId { get; set; }

        public string Password { get; set; }
    }
}
