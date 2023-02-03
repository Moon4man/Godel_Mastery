using AutoMapper;
using Lab06.MVC.Core.DTO;
using Lab06.MVC.Infrastructure.Data.Models;

namespace Lab06.MVC.Core
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
