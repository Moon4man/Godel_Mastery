using Lab06.MVC.Core.Interfaces;

namespace Lab06.MVC.Web.ViewModels
{
    public class ShopCartViewModel
    {
        public IShopCartService ShopCart { get; set; }
        public decimal ShopCartTotal { get; set; }
    }
}
