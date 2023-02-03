using Lab06.MVC.Core.DTO;

namespace Lab06.MVC.Web.ViewModels
{
    public class BookViewModel
    {
        public BookDTO Book { get; set; }
        public CategoryDTO Category { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
