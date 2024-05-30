using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class UserDetailVM
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public bool TowFacotrEnabled { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int AccessFailesCount { get; set; }
    }
    public class AdminRegsister
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string FullName { get; set; }
        public string role { get; set; }
    }
}
