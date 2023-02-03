using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Web.Controllers
{
    [Authorize]
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BookController(ILogger<BookController> logger, 
            IBookService bookService, 
            ICategoryService categoryService)
        {
            _logger = logger;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("The Books admin tools page is open!");
            return View(_bookService.GetBooks().ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            BookViewModel viewModel = new BookViewModel
            {
                Categories = _categoryService.GetCategories()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(BookDTO book)
        {
            _bookService.Add(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                var book = _bookService.GetById(id);
                if (book != null)
                {
                    BookViewModel viewModel = new BookViewModel
                    {
                        Book = book,
                        Categories = _categoryService.GetCategories()
                    };
                    return View(viewModel);
                }
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(BookDTO book)
        {
            _bookService.Update(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            if (id != null)
            {
                var book = _bookService.GetById(id);
                if (book != null)
                    return View(book);
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                var book = _bookService.GetById(id);
                if (book != null)
                {
                    _bookService.Remove(book);
                    return RedirectToAction("Index");
                }
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
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
    }
}
