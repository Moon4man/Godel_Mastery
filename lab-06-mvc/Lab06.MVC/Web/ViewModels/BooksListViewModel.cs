using Lab06.MVC.Core.DTO;

namespace Lab06.MVC.Web.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<BookDTO> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
