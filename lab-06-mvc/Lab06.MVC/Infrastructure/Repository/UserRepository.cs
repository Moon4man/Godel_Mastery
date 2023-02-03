using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopDBContext _context;
        public UserRepository(ShopDBContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
