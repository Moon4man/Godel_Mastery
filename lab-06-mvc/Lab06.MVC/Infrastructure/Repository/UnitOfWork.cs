using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<BookRepository> _loggerForBook;
        private readonly ILogger<CategoryRepository> _loggerForCategory;
        private readonly ILogger<ShopCartItemRepository> _loggerForShopCart;
        private readonly ILogger<OrderDetailRepository> _loggerForOrderDetail;
        private readonly ILogger<OrderRepository> _loggerForOrder;
        private readonly ShopDBContext _content;
        private BookRepository _bookRepository;
        private CategoryRepository _categoryRepository;
        private UserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;
        private ShopCartItemRepository _shopCartItemRepository;
        private OrderDetailRepository _orderDetailRepository;
        private OrderRepository _orderRepository;

        public UnitOfWork(
            ILogger<BookRepository> loggerForBook,
            ILogger<CategoryRepository> loggerForCategory,
            ILogger<ShopCartItemRepository> loggerForShopCart,
            ILogger<OrderDetailRepository> loggerForOrderDetail,
            ILogger<OrderRepository> loggerForOrder,
            ShopDBContext content)
        {
            _loggerForBook = loggerForBook;
            _loggerForCategory = loggerForCategory;
            _loggerForShopCart = loggerForShopCart;
            _loggerForOrderDetail = loggerForOrderDetail;
            _loggerForOrder = loggerForOrder;
            _content = content;
        }

        public IBookRepository Book
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_loggerForBook, _content);
                return _bookRepository;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_loggerForCategory, _content);
                return _categoryRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if(_userRepository == null)
                    _userRepository = new UserRepository(_content);
                return _userRepository;
            }
        }

        public IAuthenticationRepository Authentication
        {
            get
            {
                if(_authenticationRepository == null)
                    _authenticationRepository = new AuthenticationRepository(_content);
                return _authenticationRepository;
            }
        }

        public IShopCartItemRepository ShopCartItem
        {
            get
            {
                if (_shopCartItemRepository == null)
                    _shopCartItemRepository = new ShopCartItemRepository(_loggerForShopCart, _content);
                return _shopCartItemRepository;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_loggerForOrder, _content);
                return _orderRepository;
            }
        }

        public IOrderDetailRepository OrderDetail
        {
            get
            {
                if (_orderDetailRepository == null)
                    _orderDetailRepository = new OrderDetailRepository(_loggerForOrderDetail, _content);
                return _orderDetailRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _content.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
