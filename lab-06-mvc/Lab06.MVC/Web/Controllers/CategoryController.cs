using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Web.Controllers
{
    [Authorize]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, 
            ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("The Categories admin tools page is open!");
            return View(_categoryService.GetCategories().ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryDTO category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                var category = _categoryService.GetById(id);
                if (category != null)
                {
                    return View(category);
                }
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(CategoryDTO category)
        {
            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            if (id != null)
            {
                var category = _categoryService.GetById(id);
                if (category != null)
                    return View(category);
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                var category = _categoryService.GetById(id);
                if (category != null)
                {
                    _categoryService.Delete(category);
                    return RedirectToAction("Index");
                }
            }
            _logger.LogWarning($"Id not passed. id={id}");
            return NotFound();
        }

        public IActionResult Search(string result)
        {
            var categories = _categoryService.GetCategories();
            _logger.LogDebug("Geting categories from the DB");

            if (!String.IsNullOrEmpty(result))
            {
                categories = categories.Where(c => c.Name!.Contains(result));
                _logger.LogInformation("Search for a category by entering a string in the title");
            }

            _logger.LogInformation("Display found categories on the screen");
            return View(categories.ToList());
        }
    }
}
