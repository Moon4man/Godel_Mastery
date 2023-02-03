namespace Lab06.MVC.Infrastructure.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public int? YearOfPublication { get; set; }
        public int? NumberOfPage { get; set; }
        public string? Description { get; set; }
        public string? ImageLink { get; set; }
        public decimal Price { get; set; }
        public bool IsFavorite { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
