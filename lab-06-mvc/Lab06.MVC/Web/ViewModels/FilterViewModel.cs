namespace Lab06.MVC.Web.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(int? category)
        {
            SelectedCategory = category;
        }
        public int? SelectedCategory { get; private set; }
    }
}
