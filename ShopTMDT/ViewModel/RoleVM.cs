using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class RoleVM
    {
        [Required]
        public string name { get; set; }
    }
    public class RoleResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? TotalUser { get; set; }
    }
}
