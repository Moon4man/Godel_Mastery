using System.Collections.Generic;
using System.Linq;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lab06.MVC.Web.Tests.Controllers
{
    public class BookControllerTests
    {
        [Fact]
        public void Index_ReturnViewResult_ListOfAllBooks()
        {
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetBooks()).Returns(GetTestBooks());
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BookDTO>>(viewResult.Model);
            Assert.Equal(GetTestBooks().Count, model.Count());
        }

        private List<BookDTO> GetTestBooks()
        {
            var books = new List<BookDTO>
            {
                new BookDTO
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
                new BookDTO
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
                new BookDTO
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
                    IsFavorite = true
                }
            };
            return books;
        }

        [Fact]
        public void Add_ReturnARedirect_AddBook()
        {
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);
            BookDTO newBook = new BookDTO();

            var result = controller.Add(newBook);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(a => a.Add(newBook));
        }

        [Fact]
        public void EditBook_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.Edit(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditBook_ReturnNotFound_BookNotFound()
        {
            int testId = 1;
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);
            mock.Setup(service => service.GetById(testId)).Returns(null as BookDTO);

            var result = controller.Edit(testId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditBook_ReturnNotFound_BookIsNull()
        {
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);
            BookDTO editBook = new BookDTO();

            var result = controller.Edit(editBook);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ConfirmDelete_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.ConfirmDelete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ConfirmDelete_ReturnNotFound_BookNotFound()
        {
            int testFailedId = -1;
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetById(testFailedId)).Returns(null as BookDTO);
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.ConfirmDelete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ConfirmDelete_ReturnaActionResult_WithBook()
        {
            int testId = 1;
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetById(testId)).Returns(GetTestBooks().FirstOrDefault(p => p.Id == testId));
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.ConfirmDelete(testId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BookDTO>(viewResult.ViewData.Model);
            Assert.Equal("Портрет Дориана Грея", model.Name);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public void DeleteBook_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.Delete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteBook_ReturnNotFound_BookNotFound()
        {
            int testFailedId = -1;
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);
            mock.Setup(service => service.GetById(testFailedId)).Returns(null as BookDTO);

            var result = controller.Delete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCategory_ReturnARedirect_WithCategory()
        {
            int testId = 1;
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetById(testId))
                .Returns(GetTestBooks().FirstOrDefault(a => a.Id == testId));
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.Delete(testId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void SearchBook_ReturnViewResult_ResultIsNull()
        {
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.Search(null);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SearchBook_ReturnViewResult_ResultNotNull()
        {
            var mock = new Mock<IBookService>();
            var controller = new BookController(new Mock<ILogger<BookController>>().Object, mock.Object, new Mock<ICategoryService>().Object);

            var result = controller.Search("Портрет Дориана Грея");

            Assert.IsType<ViewResult>(result);
        }
    }
}