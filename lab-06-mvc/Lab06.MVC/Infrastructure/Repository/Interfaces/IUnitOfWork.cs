namespace Lab06.MVC.Infrastructure.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Book { get; }
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IAuthenticationRepository Authentication { get; }
        IShopCartItemRepository ShopCartItem { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; }
    }
}
