using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Core.Interfaces
{
    public interface IShopCartService
    {
        string ShopCartId { get; set; }
        List<ShopCartItem> ListShopItem { get; set; }
        void AddToCart(Book book, int amount);
        void RemoveFromCart(Book book);
        List<ShopCartItem> GetShopCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        Task<CardSummary> GetCartSummaryAsync();
    }
}
