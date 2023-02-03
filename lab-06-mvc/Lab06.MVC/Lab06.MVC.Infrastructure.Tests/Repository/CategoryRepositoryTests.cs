using System;
using System.Linq;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lab06.MVC.Infrastructure.Tests.Repository
{
    public class CategoryRepositoryTests
    {
        private readonly DataFixture fixture = new DataFixture();

        [Fact]
        public void GetAll_ReturnAllCategories()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);

            var result = repository.GetAll();

            Assert.Equal(3, result.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Get_ReturnCategory()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);

            var result = repository.Get(1);

            Assert.Equal("Foreign modern literature", result.Name);
            fixture.Dispose();
        }

        [Fact]
        public void Get_ReturnCategory_IdIsNull()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);

            var result = repository.Get(-1);

            Assert.Null(result);
            fixture.Dispose();
        }

        [Fact]
        public void Add_ReturnOne_AddCategoryInDb()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);
            Category category = new Category()
            {
                Id = 4,
                Name = "TestCategory",
            };

            var result = repository.Add(category);

            Assert.Equal(1, result);
            Assert.Equal(4, fixture.Context.Category.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Add_ReturnMinusOne_AddCategoryWithIdAlreadyExistsInDb()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);
            Category category = new Category()
            {
                Id = 1,
                Name = "TestCategory",
            };

            var result = repository.Add(category);

            Assert.Equal(-1, result);
            Assert.Equal(3, fixture.Context.Category.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Update_ReturnOne_UpdateCategoryInDb()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);
            var category = fixture.Context.Category.FirstOrDefault(x => x.Id == 1);

            var result = repository.Update(category);

            Assert.Equal(1, result);
            Assert.Equal(3, fixture.Context.Category.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Update_ReturnMinusOne_UpdateCategoryWithNonExistentIdInDb()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);
            var category = fixture.Context.Category.FirstOrDefault(x => x.Id == -1);

            var result = repository.Update(category);

            Assert.Equal(-1, result);
            Assert.Equal(3, fixture.Context.Category.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Delete_DeleteCategoryFromDb()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);
            var category = fixture.Context.Category.FirstOrDefault(x => x.Id == 1);

            repository.Delete(category);

            Assert.Equal(2, fixture.Context.Category.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Delete_DeleteCategoryWithNonExistentIdFromDb()
        {
            CategoryRepository repository = new CategoryRepository(new Mock<ILogger<CategoryRepository>>().Object, fixture.Context);
            var category = fixture.Context.Category.FirstOrDefault(x => x.Id == -1);

            Action act = () => repository.Delete(category);

            Assert.Throws<ArgumentNullException>(act);
            Assert.Equal(3, fixture.Context.Category.Count());
            fixture.Dispose();
        }
    }
}
