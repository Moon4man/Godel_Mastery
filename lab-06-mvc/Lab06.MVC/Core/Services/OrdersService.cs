using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ILogger<OrdersService> _logger;
        private readonly ShopDBContext _context;
        private readonly IShopCartService _shopCart;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersService(ILogger<OrdersService> logger, 
            ShopDBContext context, 
            IShopCartService shopCart, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _shopCart = shopCart;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(OrderDTO order)
        {
            var orders = _mapper.Map<Order>(order);
            orders.OrderTime = DateTime.Now;
            _logger.LogDebug($"Order time: {orders.OrderTime}");
            decimal totalPrice = 0M;

            _logger.LogDebug("Getting books from the user's cart");
            var shoppingCartItems = _shopCart.ListShopItem;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = shoppingCartItem.Quantity,
                    BookId = shoppingCartItem.Book.Id,
                    Order = orders,
                    Price = shoppingCartItem.Book.Price,

                };
                totalPrice += orderDetail.Price * orderDetail.Quantity;

                _unitOfWork.OrderDetail.Add(orderDetail);
                _logger.LogDebug("Adding order details to the DB");
            }

            orders.OrderTotal = totalPrice;
            _logger.LogDebug($"Total cost of the order: {totalPrice}");
            _unitOfWork.Order.Add(orders);
            _logger.LogDebug("Added order to the DB");
            _unitOfWork.OrderDetail.Save();
        }

        public IEnumerable<OrdersViewDTO> GetAll()
        {
            return _context.Order
                .Include(e => e.OrderDetails)
                .Select(e => new OrdersViewDTO
                {
                    OrderTime = e.OrderTime,
                    OrderTotal = e.OrderTotal,
                    OrderDetails = new OrderDTO
                    {
                        Address = e.Address,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber
                    },
                    BookOrderInfo = e.OrderDetails.Select(o => new BookOrderInfoDTO
                    {
                        Name = o.Book.Name,
                        Price = o.Price,
                        Quantity = o.Quantity
                    })
                })
                .ToList();

        }
    }
}
