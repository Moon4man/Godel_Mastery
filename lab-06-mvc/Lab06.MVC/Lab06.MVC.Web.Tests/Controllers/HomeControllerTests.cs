using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lab06.MVC.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void SearchBook_ReturnViewResult_ResultIsNull()
        {
            var mock = new Mock<IBookService>();
            var controller = new HomeController(new Mock<ILogger<HomeController>>().Object, mock.Object,
                new Mock<IMapper>().Object);

            var result = controller.Search(null);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SearchBook_ReturnViewResult_ResultNotNull()
        {
            var mock = new Mock<IBookService>();
            var controller = new HomeController(new Mock<ILogger<HomeController>>().Object, mock.Object,
                new Mock<IMapper>().Object);

            var result = controller.Search("Портрет");

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetBookInfo_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<IBookService>();
            var controller = new HomeController(new Mock<ILogger<HomeController>>().Object, mock.Object,
                new Mock<IMapper>().Object);

            var result = controller.GetBookInfo(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        //[Fact]
        //public void GetBookInfo_ReturnNotFound_BookNotFound()
        //{
        //    int testFailedId = -1;
        //    var mock = new Mock<IBookService>();
        //    mock.Setup(service => service.GetById(testFailedId)).Returns(null as BookDTO);
        //    var controller = new HomeController(new Mock<ILogger<HomeController>>().Object, mock.Object,
        //        new Mock<IMapper>().Object);

        //    var result = controller.GetBookInfo(testFailedId);

        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public void GetBookInfo_ReturnViewResult_WithBook()
        //{
        //    int testId = 1;
        //    var mock = new Mock<IBookService>();
        //    mock.Setup(service => service.GetById(testId)).Returns(GetTestBooks().FirstOrDefault(a => a.Id == testId));
        //    var controller = new HomeController(new Mock<ILogger<HomeController>>().Object, mock.Object,
        //        new Mock<IMapper>().Object);

        //    var result = controller.GetBookInfo(testId);

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsType<BookDTO>(viewResult.ViewData.Model);
        //    Assert.Equal("Портрет Дориана Грея", model.Name);
        //    Assert.Equal(testId, model.Id);
        //}
    }
}
