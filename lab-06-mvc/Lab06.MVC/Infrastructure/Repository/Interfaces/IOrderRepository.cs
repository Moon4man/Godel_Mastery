using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Infrastructure.Repository.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
    }
}
