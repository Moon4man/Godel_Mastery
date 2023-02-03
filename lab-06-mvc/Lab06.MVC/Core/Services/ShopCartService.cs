using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Core.Interfaces
{
    public class ShopCartService : IShopCartService
    {
        private readonly ILogger<ShopCartService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ShopCartService(ILogger<ShopCartService> logger, 
            IUnitOfWork uniOfWork)
        {
            _logger = logger;
            _unitOfWork = uniOfWork;
        }

        public string ShopCartId { get; set; }
        public List<ShopCartItem> ListShopItem { get; set; }

        public static ShopCartService GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var logger = services.GetService<ILogger<ShopCartService>>();
            var unitOfWork = services.GetService<IUnitOfWork>();
            string shopCarId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCarId);

            return new ShopCartService(logger, unitOfWork) { ShopCartId = shopCarId };
        }

        public void AddToCart(Book book, int amount)
        {
            var shoppingCartItem = _unitOfWork.ShopCartItem.GetSingleOrDefault(book.Id, ShopCartId);
            _logger.LogInformation($"Got the book with ID {book.Id} from shop cart");

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShopCartItem
                {
                    CartId = ShopCartId,
                    Book = book,
                    Quantity = 1
                };

                _unitOfWork.ShopCartItem.Add(shoppingCartItem);
                _logger.LogInformation("Added the book in shop cart: {@book}", book);
            }
            else
            {
                shoppingCartItem.Quantity++;
                _unitOfWork.ShopCartItem.Update(shoppingCartItem);
                _logger.LogInformation("Updated the quantity of book in shop cart: {@book}", book);
            }
        }

        public void RemoveFromCart(Book book)
        {
            var shoppingCartItem = _unitOfWork.ShopCartItem.GetSingleOrDefault(book.Id, ShopCartId);
            _logger.LogInformation($"Got the book with ID {book.Id} from shop cart");

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    _unitOfWork.ShopCartItem.Update(shoppingCartItem);
                    _logger.LogInformation("Reduced the quantity of book in shop cart: {@book}", book);
                }
                else
                {
                    _unitOfWork.ShopCartItem.Remove(shoppingCartItem);
                    _logger.LogInformation("Deleted the book from shop cart: {@book}", book);
                }
            }
        }

        public List<ShopCartItem> GetShopCartItems()
        {
            _logger.LogInformation("Geting the all books from shop cart");
            return _unitOfWork.ShopCartItem.GetShopCartItems(ShopCartId);
        }

        public void ClearCart()
        {
            var cartItems = _unitOfWork.ShopCartItem.GetShopCartItems(ShopCartId);

            _unitOfWork.ShopCartItem.RemoveRange(cartItems);
            _logger.LogInformation("Cleared all books from shop cart");
        }

        public decimal GetShoppingCartTotal()
        {
            _logger.LogInformation("Geting the total price of books in cart shop");
            return _unitOfWork.ShopCartItem.GetShopCartTotal(ShopCartId);
        }

        public async Task<CardSummary> GetCartSummaryAsync()
        {
            var subTotal = ListShopItem?
                .Select(c => c.Book.Price * c.Quantity) ??
                await _unitOfWork.ShopCartItem.GetShopCartTotalItems(ShopCartId);
            _logger.LogInformation("Got the total books in shop cart");
            var cartSummary = new CardSummary
            {
                ItemCount = subTotal.Count(),
                TotalAmmount = subTotal.Sum(),
            };
            return cartSummary;
        }
    }
}