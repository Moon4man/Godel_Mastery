using AutoMapper;
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
    public class CategoryServiceTests
    {
        [Fact]
        public void GetCategory_ReturnAllCategories_ListOfAllCategories()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Category.GetAll()).Returns(GetTestCategories());
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new CategoryService(mock.Object, mapper, new Mock<ILogger<CategoryService>>().Object);

            var result = service.GetCategories();

            Assert.Equal(3, result.Count());
        }

        private List<Category> GetTestCategories()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Foreign modern literature"
                },
                new Category
                {
                    Id = 2,
                    Name = "Fantasy"
                },
                new Category
                {
                    Id = 3,
                    Name = "Marvel Comics"
                }
            };
            return categories;
        }

        [Fact]
        public void GetById_ReturnCategory_WithCategory()
        {
            int id = 2;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Category.Get(id)).Returns(GetTestCategories().FirstOrDefault(x => x.Id == id));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new CategoryService(mock.Object, mapper, new Mock<ILogger<CategoryService>>().Object);

            var result = service.GetById(id);

            Assert.Equal("Fantasy", result.Name);
        }

        [Fact]
        public void GetById_ReturnCategory_IdIsNull()
        {
            int id = -1;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Category.Get(id)).Returns(GetTestCategories().FirstOrDefault(x => x.Id == id));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new CategoryService(mock.Object, mapper, new Mock<ILogger<CategoryService>>().Object);

            var result = service.GetById(id);

            Assert.Null(result);
        }
    }
}
