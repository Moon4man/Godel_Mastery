using Lab06.MVC.Core.DTO;

namespace Lab06.MVC.Core.Interfaces
{
    public interface IOrdersService
    {
        void Create(OrderDTO order);
        IEnumerable<OrdersViewDTO> GetAll();
    }
}
