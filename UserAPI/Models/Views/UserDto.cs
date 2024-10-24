namespace UserAPI.Models.DTO
{
    public class UserDto
    {
        // This is to display user details
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        /*public string EmailAddress { get; set; }*/
        public string Password { get; set; }
        public long ContactNumber { get; set; }
        public string Address { get; set; }
        public long Pincode { get; set; }
        public long AadhaarCard { get; set; }
        public int? BrokerId { get; set; }
    }
}
