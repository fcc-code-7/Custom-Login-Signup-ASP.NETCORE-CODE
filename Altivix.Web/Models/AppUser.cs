using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Altivix.Web.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Company { get; set; }
        [Required]
        public bool IsDisabled { get; set; } = false;
    }
}
