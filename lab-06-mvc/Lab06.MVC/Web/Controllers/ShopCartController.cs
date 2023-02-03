using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Web.Controllers
{
    [Authorize]
    public class ShopCartController : Controller
    {
        private readonly ILogger<ShopCartController> _logger;
        private readonly IBookService _bookService;
        private readonly IShopCartService _shopCart;

        public ShopCartController(ILogger<ShopCartController> logger, 
            IShopCartService shopCart, 
            IBookService bookService)
        {
            _logger = logger;
            _shopCart = shopCart;
            _bookService = bookService;
        }

        public ViewResult Index()
        {
            var items = _shopCart.GetShopCartItems();
            _shopCart.ListShopItem = items;

            var obj = new ShopCartViewModel
            {
                ShopCart = _shopCart,
                ShopCartTotal = _shopCart.GetShoppingCartTotal()
            };
            _logger.LogInformation("The Shop Cart page is open!");
            return View(obj);
        }

        public RedirectToActionResult AddToCart(int id)
        {
            _logger.LogInformation($"Adding a book with ID {id} to the cart");
            var item = _bookService.GetBooksForCart().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _shopCart.AddToCart(item, 1);
            }
            _logger.LogInformation($"Added a book with ID {id} to the cart");
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _logger.LogInformation($"Deleting a book with ID {id} from the cart");
            var item = _bookService.GetBooksForCart().FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _shopCart.RemoveFromCart(item);
            }
            _logger.LogInformation($"Deleted a book with ID {id} from the cart");
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _logger.LogInformation($"Clearing the cart");
            _shopCart.ClearCart();
            _logger.LogInformation($"Cleared the cart");
            return RedirectToAction("Index");
        }
    }
}
