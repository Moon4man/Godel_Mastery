using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Core.Services;
using Lab06.MVC.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lab06.MVC.Web.Tests.Controllers
{
    public class CategoryControllerTests
    {
        [Fact]
        public void Index_ReturnViewResult_ListOfAllCategories()
        {
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetCategories()).Returns(GetTestCategories());
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(viewResult.Model);
            Assert.Equal(GetTestCategories().Count, model.Count());
        }

        private List<CategoryDTO> GetTestCategories()
        {
            var categories = new List<CategoryDTO>
            {
                new CategoryDTO
                {
                    Id = 1,
                    Name = "Fantasy",
                },
                new CategoryDTO
                {
                    Id = 2,
                    Name = "Detective",
                },
                new CategoryDTO
                {
                    Id = 3,
                    Name = "Manga",
                }
            };
            return categories;
        }

        [Fact]
        public void AddCategory_ReturnARedirect_AddNewCategory()
        {
            var mock = new Mock<ICategoryService>();
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);
            CategoryDTO categoryDTO = new CategoryDTO();

            var result = controller.Add(categoryDTO);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(a => a.Add(categoryDTO));
        }

        [Fact]
        public void EditCategory_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<ICategoryService>();
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Edit(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditCategory_ReturnNorFound_CategoryNotFound()
        {
            int testFailedId = -1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetById(testFailedId)).Returns(null as CategoryDTO);
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Edit(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditCategory_ReturnViewResult_WithCategories()
        {
            int testId = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetById(testId))
                .Returns(GetTestCategories().FirstOrDefault(a => a.Id == testId));
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Edit(testId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CategoryDTO>(viewResult.ViewData.Model);
            Assert.Equal("Fantasy", model.Name);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public void ConfirmDelete_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<ICategoryService>();
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.ConfirmDelete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ConfirmDelete_ReturnNotFound_CategoryNotFound()
        {
            int testFailedId = -1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetById(testFailedId)).Returns(null as CategoryDTO);
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.ConfirmDelete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ConfirmDelete_ReturnViewResult_WithCategory()
        {
            int testId = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetById(testId))
                .Returns(GetTestCategories().FirstOrDefault(a => a.Id == testId));
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.ConfirmDelete(testId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CategoryDTO>(viewResult.ViewData.Model);
            Assert.Equal("Fantasy", model.Name);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public void DeleteCategory_ReturnNotFound_IdIsNull()
        {
            int testFailedId = -1;
            var mock = new Mock<ICategoryService>();
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Delete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCategory_ReturnNotFound_CategoryNotFound()
        {
            int testFailedId = -1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetById(testFailedId)).Returns(null as CategoryDTO);
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Delete(testFailedId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCategory_ReturnARedirect_WithCategory()
        {
            int testId = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(service => service.GetById(testId))
                .Returns(GetTestCategories().FirstOrDefault(a => a.Id == testId));
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Delete(testId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void SearchCategory_ReturnViewModel_ResultIsNull()
        {
            var mock = new Mock<ICategoryService>();
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Search(null);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SearchBook_ReturnViewResult_ResultNotNull()
        {
            var mock = new Mock<ICategoryService>();
            var controller = new CategoryController(new Mock<ILogger<CategoryController>>().Object, mock.Object);

            var result = controller.Search("Fantasy");

            Assert.IsType<ViewResult>(result);
        }
    }
}
