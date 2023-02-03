using Lab06.MVC.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Lab06.MVC.Infrastructure.Data
{
    public class ShopDBContext : DbContext
    {
        public DbSet<Book> Book { get; set; } = null;
        public DbSet<Category> Category { get; set; } = null;
        public DbSet<Authentication> Authentication { get; set; } = null;
        public DbSet<User> User { get; set; } = null;
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        public ShopDBContext(DbContextOptions<ShopDBContext> options)
            : base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>(BookConfigure);
            builder.Entity<Category>(CategoryBuilder);
            builder.Entity<Authentication>(AuthenticationBuilder);
            builder.Entity<User>(UserBuilder);
            builder.Entity<Order>(OrderBuilder);
            builder.Entity<OrderDetail>(OrderDetailBuilder);
            builder.Entity<ShopCartItem>(ShopCartItemBuilder);
        }

        public void BookConfigure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Author)
                .HasMaxLength(70)
                .IsRequired();

            builder
                .Property(p => p.PublishingHouse)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasMaxLength(1500);

            builder
                .Property(p => p.ImageLink)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .HasColumnType("MONEY")
                .IsRequired();

            builder
               .Property(p => p.IsFavorite)
               .IsRequired();
        }

        public void CategoryBuilder(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
        }

        public void AuthenticationBuilder(EntityTypeBuilder<Authentication> builder)
        {
            builder
                .Property(p => p.UserName)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(p => p.PasswordHash)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(p => p.PasswordSalt)
                .HasMaxLength(250)
                .IsRequired();
        }

        public void UserBuilder(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(p => p.DateOfBirth)
                .IsRequired();

            builder
                .Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        }

        public void ShopCartItemBuilder(EntityTypeBuilder<ShopCartItem> builder)
        {
            builder
                .Property(p => p.CartId)
                .HasMaxLength(100)
                .IsRequired();
        }

        public void OrderBuilder(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(p => p.FirstName)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property<string>(p => p.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(p => p.Address)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.PhoneNumber)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(p => p.OrderTotal)
                .HasColumnType("MONEY");

            builder
                .Property(p => p.ClientName)
                .HasMaxLength(30)
                .IsRequired();
        }

        public void OrderDetailBuilder(EntityTypeBuilder<OrderDetail> builder)
        {
            builder
                .Property(p => p.Price)
                .HasColumnType("MONEY")
                .IsRequired();
        }
    }
}
