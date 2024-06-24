using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Altivix.Web.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Password don't Match")]  
        [Compare("Password" , ErrorMessage = "Password don't Match")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Company { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
