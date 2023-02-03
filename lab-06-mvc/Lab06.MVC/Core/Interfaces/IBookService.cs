using Lab06.MVC.Core.DTO;
using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Core.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookDTO> GetBooks();
        IEnumerable<Book> GetBooksForCart();
        IEnumerable<BookDTO> GetFavBooks();
        BookDTO GetById(int id);
        void Add(BookDTO book);
        void Update(BookDTO book);
        void Remove(BookDTO book);
    }
}
