namespace Lab06.MVC.Core.DTO
{
    public class OrdersViewDTO
    {
        public OrderDTO OrderDetails { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? OrderTime { get; set; }
        public IEnumerable<BookOrderInfoDTO> BookOrderInfo { get; set; }
    }
}
