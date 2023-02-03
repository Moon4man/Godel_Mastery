using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Core.Interfaces;
using Lab06.MVC.Infrastructure.Data.Models;
using Lab06.MVC.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lab06.MVC.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_unitOfWork.Category.GetAll());
        }

        public CategoryDTO GetById(int id)
        {
            _logger.LogDebug("Geting the category from the database by Id");
            _logger.LogDebug($"Get the category with Id: {id}");
            return _mapper.Map<CategoryDTO>(_unitOfWork.Category.Get(id));
        }

        public void Add(CategoryDTO category)
        {
            var categories = _mapper.Map<Category>(category);
            _unitOfWork.Category.Add(categories);
            _logger.LogDebug($"Added the category with Id: {category.Id}");
        }

        public void Update(CategoryDTO category)
        {
            var categories = _mapper.Map<Category>(category);
            _unitOfWork.Category.Update(categories);
            _logger.LogDebug($"Updated the category with Id: {category.Id}");
        }

        public void Delete(CategoryDTO category)
        {
            var categories = _mapper.Map<Category>(category);
            _unitOfWork.Category.Delete(categories);
            _logger.LogDebug($"Deleted the category with Id: {category.Id}");
        }
    }
}
