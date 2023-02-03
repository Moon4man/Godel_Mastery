using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, 
            IBookService bookService, 
            IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        public ViewResult Index()
        {
            _logger.LogInformation("The Home page is open!");
            var books = _mapper.Map<IEnumerable<BookDTO>>(_bookService.GetFavBooks());
            _logger.LogDebug("Geting books from the DB");
            _logger.LogInformation("Display books on the screen");
            return View(books);
        }

        public IActionResult GetBooks(int? category, int page = 1)
        {
            int pageSize = 9;

            var books = _bookService.GetBooks();
            _logger.LogDebug("Geting books from the DB");

            if (category != null && category != 0)
            {
                books = books.Where(x => x.CategoryId == category);
            }

            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            BooksListViewModel viewModel = new BooksListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                FilterViewModel = new FilterViewModel(category),
                Books = items
            };
            _logger.LogInformation("Display books on the screen");
            return View(viewModel);
        }

        public IActionResult Search(string result)
        {
            var books = _bookService.GetBooks();
            _logger.LogDebug("Geting books from the DB");

            if (!String.IsNullOrEmpty(result))
            {
                books = books.Where(b => b.Name!.Contains(result) || b.Author!.Contains(result));
                _logger.LogInformation("Search for a book by entering a string in the title");
            }

            _logger.LogInformation("Display found books on the screen");
            return View(books.ToList());
        }

        public IActionResult GetBookInfo(int id)
        {
            if (id != null)
            {
                var book = _bookService.GetBooks().FirstOrDefault(b => b.Id == id);
                _logger.LogDebug("Getting the selected book from the database");
                if (book != null)
                {
                    _logger.LogInformation("Output information about the selected book on the screen");
                    return View(book);
                }
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
        }
    }
}