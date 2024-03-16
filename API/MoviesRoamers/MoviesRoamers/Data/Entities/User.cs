using Microsoft.AspNetCore.Identity;

namespace MovieRoamers.Data.Entities
{
    public class User : IdentityUser
    {
       //public int Id { get; set; }
       // public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }
        public bool? EmailConfirmed { get; set; }
    }
}
