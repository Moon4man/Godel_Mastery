using AutoMapper;
using Lab06.MVC.Core.Services;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Lab06.MVC.Core.Tests.Services
{
    public class AuthorizationServiceTests
    {
        [Fact]
        public void Exists_ReturnTrue_CheckUserExistentInDb()
        {
            var username = "Ivan";
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Authentication.Get(username)).Returns(GetTestAuth().Find(x => x.UserName == username));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new AuthorizationService(mock.Object, mapper, new Mock<ILogger<AuthorizationService>>().Object);

            var result = service.Exists(username);

            Assert.True(result);
        }

        [Fact]
        public void Exists_ReturnFalse_CheckNonUserExistentInDb()
        {
            var username = "Alex";
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(service => service.Authentication.Get(username)).Returns(GetTestAuth().Find(x => x.UserName == username));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new AuthorizationService(mock.Object, mapper, new Mock<ILogger<AuthorizationService>>().Object);

            var result = service.Exists(username);

            Assert.False(result);
        }

        private List<Authentication> GetTestAuth()
        {
            var books = new List<Authentication>
            {
                new Authentication
                {
                    Id = 1,
                    UserName = "Ivan",
                    PasswordHash = "",
                    PasswordSalt = "",
                    Role = ""
                },
                new Authentication
                {
                    Id = 2,
                    UserName = "Olga",
                    PasswordHash = "",
                    PasswordSalt = "",
                    Role = ""
                }
            };
            return books;
        }
    }
}
