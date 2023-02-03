using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class ShopCartItemRepository : IShopCartItemRepository
    {
        private readonly ILogger<ShopCartItemRepository> _logger;
        private readonly ShopDBContext _context;
        public ShopCartItemRepository(ILogger<ShopCartItemRepository> logger, 
            ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ShopCartItem Get(int id)
        {
            _logger.LogDebug($"Got the book with ID {id} from DB");
            return _context.ShopCartItem.FirstOrDefault(b => b.Id == id);
        }

        public ShopCartItem GetSingleOrDefault(int id, string ShopCartId)
        {
            _logger.LogDebug($"Got the book with ID {id} from DB");
            return _context.ShopCartItem.AsNoTracking().SingleOrDefault(s => s.Book.Id == id && s.CartId == ShopCartId);
        }

        public List<ShopCartItem> GetShopCartItems(string ShopCartId)
        {
            _logger.LogDebug("Got the all books from DB");
            return _context.ShopCartItem.Where(c => c.CartId == ShopCartId).Include(s => s.Book).ToList();
        }

        public decimal GetShopCartTotal(string ShopCartId)
        {
            _logger.LogDebug("Got the total price of books in DB");
            return _context.ShopCartItem.Where(c => c.CartId == ShopCartId).Select(c => c.Book.Price * c.Quantity).Sum();
        }

        public async Task<IEnumerable<decimal>> GetShopCartTotalItems(string ShopCartId)
        {
            var total = await _context.ShopCartItem.Where(c => c.CartId == ShopCartId).Select(c => c.Book.Price * c.Quantity).ToListAsync();
            _logger.LogDebug("Got the total books in DB");
            return total;
        }

        public void Add(ShopCartItem shopCartItem)
        {
            _context.ShopCartItem.Add(shopCartItem);
            _context.SaveChanges();
            _logger.LogDebug("Added the book in DB: {@shopCartItem}", shopCartItem);
        }

        public void Update(ShopCartItem shopCartItem)
        {
            _context.ShopCartItem.Update(shopCartItem);
            _context.SaveChanges();
            _logger.LogDebug("Updated the quantity of book in DB: {@shopCartItem}", shopCartItem);
        }

        public void Remove(ShopCartItem shopCartItem)
        {
            _context.ShopCartItem.Remove(shopCartItem);
            _context.SaveChanges();
            _logger.LogDebug("Deleted the book from DB: {@shopCartItem}", shopCartItem);
        }

        public void RemoveRange(List<ShopCartItem> shopCartItem)
        {
            _context.ShopCartItem.RemoveRange(shopCartItem);
            _context.SaveChanges();
            _logger.LogDebug("The basket is cleared");
        }
    }
}
