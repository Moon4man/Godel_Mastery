using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ShopDBContext _context;
        public AuthenticationRepository(ShopDBContext context)
        {
            _context = context;
        }

        public Authentication Get(string username)
        {
            return _context.Authentication.FirstOrDefault(b => b.UserName == username);
        }

        public async Task Add(Authentication authentication)
        {
            _context.Authentication.Add(authentication);
            await _context.SaveChangesAsync();
        }
    }
}
