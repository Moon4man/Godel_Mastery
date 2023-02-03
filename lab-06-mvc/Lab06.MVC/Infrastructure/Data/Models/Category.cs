namespace Lab06.MVC.Infrastructure.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
        public Category()
        {
            Books = new List<Book>();
        }
    }
}
