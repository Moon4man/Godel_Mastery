using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ILogger<OrderDetailRepository> _logger;
        private readonly ShopDBContext _context;
        public OrderDetailRepository(ILogger<OrderDetailRepository> logger, 
            ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Add(OrderDetail orderDetail)
        {
            _context.OrderDetail.Add(orderDetail);
            _logger.LogDebug("Adding the order detail in DB: {@orderDetail}", orderDetail);
        }

        public void Save()
        {
            _context.SaveChanges();
            _logger.LogDebug("Added the order detail in DB");
        }
    }
}
