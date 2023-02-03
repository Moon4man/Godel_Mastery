using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly ShopDBContext _context;
        public BookRepository(ILogger<BookRepository> logger, 
            ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IEnumerable<Book> GetAll()
        {
            return _context.Book.Include(b => b.Category);
        }

        public IEnumerable<Book> GetBestBooks()
        {
            return _context.Book.Where(p => p.IsFavorite).Include(c => c.Category);
        }

        public Book Get(int id)
        {
            return _context.Book.AsNoTracking().FirstOrDefault(b => b.Id == id);
        }

        public int Add(Book book)
        {
            try
            {
                _context.Book.Add(book);
                _context.SaveChanges();
                _logger.LogDebug("Added the book in DB: {@book}", book);
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred adding the book in DB");
                return -1;
            }
        }

        public int Update(Book book)
        {
            try
            {
                _context.Book.Update(book);
                _context.SaveChanges();
                _logger.LogDebug("Updated the book in DB: {@book}", book);
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred updating the book in DB");
                return -1;
            }
        }

        public void Delete(Book book)
        {
            _context.Book.Remove(book);
            _context.SaveChanges();
            _logger.LogDebug("Deleted the book in DB: {@book}", book);
        }
    }
}
