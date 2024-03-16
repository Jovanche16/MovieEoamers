namespace MoviesRoamers.Data.Requests
{
    public class SignupRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
      
    }
}
