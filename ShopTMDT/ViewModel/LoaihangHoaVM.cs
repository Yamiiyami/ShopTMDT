using ShopTMDT.Data;
using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class LoaihangHoaVM
    {
        [Required]
        public string? TenLoai { get; set; }

        public int? IdKhuyenMai { get; set; }

    }
    public class LoaiHangHoaMD : LoaihangHoaVM
    {
        public int IdLoaiHangHoa { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();

        public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }

    }
    public class LoaiHangHoaResponse
    {
        public int IdLoaiHangHoa { get; set; }
        public string? TenLoai { get; set; }
        public int? IdKhuyenMai { get; set; }

    }

}
