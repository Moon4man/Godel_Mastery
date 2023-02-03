using Lab06.MVC.Core.DTO;
using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Core.Interfaces
{
    public interface IAuthService
    {
        Authentication Authenticate(string username, string password);
        Task AddUser(string username, string password);
        bool Exists(string username);
        Task Add(UserDTO user);
    }
}
