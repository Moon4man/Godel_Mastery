using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Infrastructure.Data.Models;
using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Core.Services
{
    public class BookService : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(ILogger<BookService> logger, 
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<BookDTO> GetBooks()
        {
            return _mapper.Map<IEnumerable<BookDTO>>(_unitOfWork.Book.GetAll());
        }

        public IEnumerable<Book> GetBooksForCart()
        {
            return _unitOfWork.Book.GetAll();
        }

        public IEnumerable<BookDTO> GetFavBooks()
        {
            return _mapper.Map<IEnumerable<BookDTO>>(_unitOfWork.Book.GetBestBooks());
        }

        public BookDTO GetById(int id)
        {
            _logger.LogDebug("Geting the book from the database by Id");
            _logger.LogDebug($"Get the book with Id: {id}");
            return _mapper.Map<BookDTO>(_unitOfWork.Book.Get(id));
        }

        public void Add(BookDTO book)
        {
            var books = _mapper.Map<Book>(book);
            //var categories = unitOfWork.Category.GetAll; 
            //books.Category = categories.FirstOrDefault(x => x.Name == books.Category.Name);
            _unitOfWork.Book.Add(books);
            _logger.LogDebug("Added the book: {@book}", book);
        }

        public void Update(BookDTO book)
        {
            var books = _mapper.Map<Book>(book);
            _unitOfWork.Book.Update(books);
            _logger.LogDebug("Updated the book: {@book}", book);
        }

        public void Remove(BookDTO book)
        {
            var books = _mapper.Map<Book>(book);
            _unitOfWork.Book.Delete(books);
            _logger.LogDebug("Deleted the book: {@book}", book);
        }
    }
}
