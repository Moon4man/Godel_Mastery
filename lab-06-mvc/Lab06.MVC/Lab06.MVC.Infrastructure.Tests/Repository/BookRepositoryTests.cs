using System;
using System.Linq;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lab06.MVC.Infrastructure.Tests.Repository
{
    public class BookRepositoryTests
    {
        private readonly DataFixture fixture = new DataFixture();

        [Fact]
        public void Get_ReturnBook()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);

            var result = repository.Get(1);

            Assert.Equal("Портрет Дориана Грея", result.Name);
            fixture.Dispose();
        }

        [Fact]
        public void Get_ReturnBook_IdIsNull()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);

            var result = repository.Get(-1);

            Assert.Null(result);
            fixture.Dispose();
        }

        [Fact]
        public void GetAll_ReturnAllBooks()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);

            var result = repository.GetAll();

            Assert.Equal(3, result.Count());
            fixture.Dispose();
        }

        [Fact]
        public void GetBestBooks_ReturnBestBooks()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);

            var result = repository.GetBestBooks();

            Assert.Equal(2, result.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Add_ReturnOne_AddBookInDb()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);
            Book book = new Book()
            {
                Id = 4,
                Name = "TestBook",
                Author = "TestAuthor",
                PublishingHouse = "TestHouse",
                YearOfPublication = 2019,
                NumberOfPage = 320,
                Description = "Off",
                ImageLink = "",
                Price = 11.3m,
                IsFavorite = true,
                CategoryId = 1
            };

            var result = repository.Add(book);

            Assert.Equal(1, result);
            Assert.Equal(4, fixture.Context.Book.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Add_ReturnMinusOne_AddBookWithIdAlreadyExistsInDb()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);
            Book book = new Book()
            {
                Id = 1,
                Name = "TestBook",
                Author = "TestAuthor",
                PublishingHouse = "TestHouse",
                YearOfPublication = 2019,
                NumberOfPage = 320,
                Description = "Off",
                ImageLink = "",
                Price = 11.3m,
                IsFavorite = true,
                CategoryId = 1
            };

            var result = repository.Add(book);

            Assert.Equal(-1, result);
            Assert.Equal(3, fixture.Context.Book.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Update_ReturnOne_UpdateBookInDb()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);
            var book = fixture.Context.Book.FirstOrDefault(x => x.Id == 1);

            var result = repository.Update(book);

            Assert.Equal(1, result);
            Assert.Equal(3, fixture.Context.Book.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Update_ReturnMinusOne_UpdateBookWithNonExistentIdInDb()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);
            var book = fixture.Context.Book.FirstOrDefault(x => x.Id == -1);

            var result = repository.Update(book);

            Assert.Equal(-1, result);
            Assert.Equal(3, fixture.Context.Book.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Delete_DeleteBookFromDb()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);
            var book = fixture.Context.Book.FirstOrDefault(x => x.Id == 1);

            repository.Delete(book);

            Assert.Equal(2, fixture.Context.Book.Count());
            fixture.Dispose();
        }

        [Fact]
        public void Delete_ReturnArgumentNullExeption_DeleteBookWithNonExistentIdFromDb()
        {
            BookRepository repository = new BookRepository(new Mock<ILogger<BookRepository>>().Object, fixture.Context);
            var book = fixture.Context.Book.FirstOrDefault(x => x.Id == -1);

            Action act = () => repository.Delete(book);

            Assert.Throws<ArgumentNullException>(act);
            Assert.Equal(3, fixture.Context.Book.Count());
            fixture.Dispose();
        }
    }
}