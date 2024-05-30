
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class KhuyenMaiVM
    {
        public string? TenKhuyenMai { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public string? MoTa { get; set; }
    }
    public class KhuyenMaiMD : KhuyenMaiVM
    {
        public int IdKhuyenMai { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();

        public virtual ICollection<LoaiHangHoa> LoaiHangHoas { get; set; } = new List<LoaiHangHoa>();

        public virtual ICollection<ThongTinXuat> ThongTinXuats { get; set; } = new List<ThongTinXuat>();
    }
}
