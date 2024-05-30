using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class LoginRepuest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
