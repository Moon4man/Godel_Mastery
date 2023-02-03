using Lab06.MVC.Core.DTO;

namespace Lab06.MVC.Core.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetById(int id);
        void Add(CategoryDTO category);
        void Update(CategoryDTO category);
        void Delete(CategoryDTO category);
    }
}
