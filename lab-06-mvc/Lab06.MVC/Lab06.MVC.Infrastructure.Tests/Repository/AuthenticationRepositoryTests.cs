using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lab06.MVC.Infrastructure.Tests.Repository
{
    public class AuthenticationRepositoryTests
    {
        private readonly DataFixture fixture = new DataFixture();

        [Fact]
        public void Get_ReturnUser()
        {
            AuthenticationRepository repository = new AuthenticationRepository(fixture.Context);
            var username = "Ivan";

            var result = repository.Get(username);

            Assert.Equal("Ivan", result.UserName);
            fixture.Dispose();
        }

        [Fact]
        public void Get_ReturnNull_GetNonExistentUserFromDb()
        {
            AuthenticationRepository repository = new AuthenticationRepository(fixture.Context);

            var result = repository.Get(null);

            Assert.Null(result);
            fixture.Dispose();
        }

        [Fact]
        public async Task Add_AddUserInDb()
        {
            AuthenticationRepository repository = new AuthenticationRepository(fixture.Context);
            var auth = new Authentication()
            {
                Id = 3,
                UserName = "Alex",
                PasswordHash = "",
                PasswordSalt = "",
                Role = ""
            };

            await repository.Add(auth);

            Assert.Equal(3, fixture.Context.Authentication.Count());
            fixture.Dispose();
        }

        [Fact]
        public async Task Add_AddUserWithAlreadyExistIdInDb()
        {
            AuthenticationRepository repository = new AuthenticationRepository(fixture.Context);
            var auth = new Authentication()
            {
                Id = 1,
                UserName = "Alex",
                PasswordHash = "",
                PasswordSalt = "",
                Role = ""
            };

            var result = repository.Add(auth);

            Assert.Equal(2, fixture.Context.Authentication.Count());
            fixture.Dispose();
        }
    }
}
