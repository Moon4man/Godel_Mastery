using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Services;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lab06.MVC.Core.Tests.Services
{
    public class BookServiceTests
    {
        [Fact]
        public void GetBooks_ReturnAllBooks_ListOfAllBooks()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Book.GetAll()).Returns(GetTestBooks());
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new BookService(new Mock<ILogger<BookService>>().Object, mapper, mock.Object);

            var result = service.GetBooks();

            Assert.Equal(3, result.Count());
        }

        private List<Book> GetTestBooks()
        {
            var books = new List<Book>
            {
                new Book
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
                    IsFavorite = true
                },
                new Book
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
                    IsFavorite = true
                },
                new Book
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
                    IsFavorite = false
                }
            };
            return books;
        }

        [Fact]
        public void GetFavBooks_ReturnFavBooks_ListOfFavBooks()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Book.GetBestBooks()).Returns(GetTestBooks().FindAll(x => x.IsFavorite == true));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new BookService(new Mock<ILogger<BookService>>().Object, mapper, mock.Object);

            var result = service.GetFavBooks();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetById_ReturnBook_WithBook()
        {
            int id = 1;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Book.Get(id)).Returns(GetTestBooks().FirstOrDefault(x => x.Id == id));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new BookService(new Mock<ILogger<BookService>>().Object, mapper, mock.Object);

            var result = service.GetById(id);

            Assert.Equal("Портрет Дориана Грея", result.Name);
        }

        [Fact]
        public void GetById_ReturnBook_IdIsNull()
        {
            int id = -1;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Book.Get(id)).Returns(GetTestBooks().FirstOrDefault(x => x.Id == id));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new BookService(new Mock<ILogger<BookService>>().Object, mapper, mock.Object);

            var result = service.GetById(id);

            Assert.Null(result);
        }
    }
}