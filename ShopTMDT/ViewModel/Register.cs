using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
