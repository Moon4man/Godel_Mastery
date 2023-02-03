using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Infrastructure.Repository.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetBestBooks();
        Book Get(int id);
        int Add(Book book);
        int Update(Book book);
        void Delete(Book book);
    }
}
