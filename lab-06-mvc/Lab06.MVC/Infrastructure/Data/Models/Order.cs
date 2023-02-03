using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Infrastructure.Data.Models
{
    public class Order
    {
        [BindNever]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public List<OrderDetail> OrderDetails { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal? OrderTotal { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime? OrderTime { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public string? ClientName { get; set; }
    }
}
