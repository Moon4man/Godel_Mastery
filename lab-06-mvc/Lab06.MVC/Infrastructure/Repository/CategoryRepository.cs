using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly ShopDBContext _context;

        public CategoryRepository(ILogger<CategoryRepository> logger,
            ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category;
        }

        public Category Get(int id)
        {
            return _context.Category.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public int Add(Category category)
        {
            try
            {
                _context.Category.Add(category);
                _context.SaveChanges();
                _logger.LogDebug("Added the category in DB: {@category}", category);
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred adding the category in DB");
                return -1;
            }
        }

        public int Update(Category category)
        {
            try
            {
                _context.Category.Update(category);
                _context.SaveChanges();
                _logger.LogDebug("Updated the category in DB: {@category}", category);
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred updating the category in DB");
                return -1;
            }
        }

        public void Delete(Category category)
        {
            _context.Category.Remove(category);
            _context.SaveChanges();
            _logger.LogDebug("Deleted the category in DB: {@category}", category);
        }
    }
}
