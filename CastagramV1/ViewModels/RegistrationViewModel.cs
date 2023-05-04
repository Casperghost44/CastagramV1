using System.ComponentModel.DataAnnotations;

namespace CastagramV1.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required!")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Your Password Please!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "-> Passwords do not match!! <-")]
        public string? ConfirmPassword { get; set; }
    }
}
