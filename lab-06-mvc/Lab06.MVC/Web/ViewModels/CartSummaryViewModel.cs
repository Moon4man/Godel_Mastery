using Lab06.MVC.Core.Interfaces;

namespace Lab06.MVC.Web.ViewModels
{
    public class CartSummaryViewModel
    {
        public IShopCartService ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public int ShoppingCartItemsTotal { get; set; }
    }
}
