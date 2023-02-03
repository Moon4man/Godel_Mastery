namespace Lab06.MVC.Core.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual BookDTO Book { get; set; }
        public virtual OrderDTO Order { get; set; }
    }
}
