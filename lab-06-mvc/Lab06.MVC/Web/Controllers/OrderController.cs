using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrdersService _orderService;
        private readonly IShopCartService _shopCartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(ILogger<OrderController> logger, 
            IOrdersService orderService, 
            IShopCartService shopCartService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _orderService = orderService;
            _shopCartService = shopCartService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            _logger.LogInformation("The Checkout page is open!");
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(OrderDTO order)
        {
            var clientName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            order.ClientName = clientName;
            _logger.LogDebug($"Added name of client in DB: {clientName}");

            var items = _shopCartService.GetShopCartItems();
            _shopCartService.ListShopItem = items;
            _logger.LogInformation("Getting all the products added to the cart");

            if (_shopCartService.ListShopItem.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some book first");
            }

            if(ModelState.IsValid)
            {
                _logger.LogInformation("Order processing...");
                _orderService.Create(order);
                _shopCartService.ClearCart();
                _logger.LogInformation("The order has been successfully processed");
                return RedirectToAction("CheckoutComplete");
            }

           _logger.LogInformation("The user entered incorrect data");
           return View(order);
        }

        [Authorize]
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = $"Thanks for your order!";
            return View();
        }

        [Authorize]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            _logger.LogInformation("Getting all the orders from DB");
            var orders = _orderService.GetAll();
            return View(orders);
        }
    }
}
