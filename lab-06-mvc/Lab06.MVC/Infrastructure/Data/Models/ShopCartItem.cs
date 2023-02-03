namespace Lab06.MVC.Infrastructure.Data.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; } 
    }
}
