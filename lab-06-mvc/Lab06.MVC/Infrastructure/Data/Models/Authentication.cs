namespace Lab06.MVC.Infrastructure.Data.Models
{
    public class Authentication
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
    }
}
