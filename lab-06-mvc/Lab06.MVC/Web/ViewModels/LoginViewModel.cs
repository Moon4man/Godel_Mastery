using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nickname")]
        [StringLength(40, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(40, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
