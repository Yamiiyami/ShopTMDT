
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class NhapHangHoaVM
    {
        public string? IdUser { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? IdNhaCungCap { get; set; }
    }
    public class NhapHangHoaMD : NhapHangHoaVM
    {
        public string? IdNhapHangHoa { get; set; }

        public virtual NhaCungCap? IdNhaCungCapNavigation { get; set; }

        public virtual User? IdUserNavigation { get; set; }

        public virtual ICollection<ThongTinDonNhap> ThongTinDonNhaps { get; set; } = new List<ThongTinDonNhap>();
    }

    public class NhaphanghoaReposet
    {
        [Required]
        public string? IdNhapHangHoa { get; set; }
        public int? IdNhaCungCap { get; set; }
        public string? IdUser { get; set; }
    }


}
