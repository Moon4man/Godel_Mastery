using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;
        private readonly ShopDBContext _context;
        public OrderRepository(ILogger<OrderRepository> logger, 
            ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
            _logger.LogDebug("Added the order in DB: {@order}", order);
        }
    }
}
