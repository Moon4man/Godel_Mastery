using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Web.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShopCartService _shoppingCart;

        public ShoppingCartSummary(IShopCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ShoppingCartCountTotal = await _shoppingCart.GetCartSummaryAsync();
            var shoppingCartViewModel = new CartSummaryViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartItemsTotal = ShoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = ShoppingCartCountTotal.TotalAmmount
            };
            return View(shoppingCartViewModel);
        }

    }
}
