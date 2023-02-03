namespace Lab06.MVC.Core.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookDTO> Books { get; set; }
        public CategoryDTO()
        {
            Books = new List<BookDTO>();
        }
    }
}
