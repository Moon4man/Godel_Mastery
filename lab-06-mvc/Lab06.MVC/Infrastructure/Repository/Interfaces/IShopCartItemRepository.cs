using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Infrastructure.Repository.Interfaces
{
    public interface IShopCartItemRepository
    {
        ShopCartItem Get(int id);
        ShopCartItem GetSingleOrDefault(int id, string ShopCartId);
        List<ShopCartItem> GetShopCartItems(string ShopCartId);
        decimal GetShopCartTotal(string ShopCartId);
        Task<IEnumerable<decimal>> GetShopCartTotalItems(string ShopCartId);
        void Add(ShopCartItem shopCartItem);
        void Update(ShopCartItem shopCartItem);
        void Remove(ShopCartItem shopCartItem);
        void RemoveRange(List<ShopCartItem> shopCartItem);
    }
}
