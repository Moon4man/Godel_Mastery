using System;
using Lab06.MVC.Infrastructure.Data;
using Lab06.MVC.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab06.MVC.Infrastructure.Tests
{
    public class DataFixture : IDisposable
    {
        public ShopDBContext Context { get; private set; }

        public DataFixture()
        {
            var options = new DbContextOptionsBuilder<ShopDBContext>()
                .UseInMemoryDatabase("eShopTest")
                .Options;

            Context = new ShopDBContext(options);

            Context.Book.Add(new Book
            {
                Id = 1,
                Name = "Портрет Дориана Грея",
                Author = "Оскар Уайльд",
                PublishingHouse = "АСТ",
                YearOfPublication = 2019,
                NumberOfPage = 320,
                Description = "Off",
                ImageLink = "",
                Price = 11.3m,
                IsFavorite = true,
                CategoryId = 1
            });
            Context.Book.Add(new Book
            {
                Id = 2,
                Name = "Маленький принц",
                Author = "Оскар Уайльд",
                PublishingHouse = "АСТ",
                YearOfPublication = 2019,
                NumberOfPage = 320,
                Description = "Off",
                ImageLink = "",
                Price = 11.3m,
                IsFavorite = true,
                CategoryId = 1
            });
            Context.Book.Add(new Book
            {
                Id = 3,
                Name = "Триумфальная арка",
                Author = "Оскар Уайльд",
                PublishingHouse = "АСТ",
                YearOfPublication = 2019,
                NumberOfPage = 320,
                Description = "Off",
                ImageLink = "",
                Price = 11.3m,
                IsFavorite = false,
                CategoryId = 1
            });

            Context.Category.Add(new Category
            {
                Id = 1,
                Name = "Foreign modern literature"
            });
            Context.Category.Add(new Category
            {
                Id = 2,
                Name = "Fantasy"
            });
            Context.Category.Add(new Category
            {
                Id = 3,
                Name = "Marvel Comics"
            });

            Context.Authentication.Add(new Authentication
            {
                Id = 1,
                UserName = "Ivan",
                PasswordHash = "",
                PasswordSalt = "",
                Role = ""
            });
            Context.Authentication.Add(new Authentication
            {
                Id = 2,
                UserName = "Olga",
                PasswordHash = "",
                PasswordSalt = "",
                Role = ""
            });

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}