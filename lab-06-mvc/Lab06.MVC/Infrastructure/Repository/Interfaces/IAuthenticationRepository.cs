using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Infrastructure.Repository.Interfaces
{
    public interface IAuthenticationRepository
    {
        Authentication Get(string username);
        Task Add(Authentication authentication);
    }
}
