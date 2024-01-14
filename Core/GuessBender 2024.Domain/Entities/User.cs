



namespace GuessBender_2024.Domain.Entities
{
    public class User 
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
