using ShopTMDT.Data;
using System.ComponentModel.DataAnnotations;

namespace ShopTMDT.ViewModel
{
    public class HoaDonXuatVM
    {

        public DateTime? NgayXuat { get; set; }

        public string? IdUser { get; set; }

        public string? DiaChi { get; set; }

        public string? GhiChu { get; set; }

        public string? IdThanhToan { get; set; }

        public string? IdVanChuyen { get; set; }

        public string? Phone { get; set; }

        public virtual TrangThaiThanhToan? IdThanhToanNavigation { get; set; }

        public virtual User? IdUserNavigation { get; set; }

        public virtual TrangThaiVanTruyen? IdVanChuyenNavigation { get; set; }

    }
    public class HoaDonXuatMD : HoaDonXuatVM
    {
        public string IdHoaDon { get; set; } = null!;

        public virtual ICollection<ThongTinXuat> ThongTinXuats { get; set; } = new List<ThongTinXuat>();

    }
    public class HoaDonRequest
    {
        [Required]
        public string? IdHoaDon { get; set; }
        public string? Phone { get; set; }
        public string? DiaChi { get; set; }
        public string? GhiChu { get; set; }
        public string? IdUser { get; set; }
    }
    public class HoaDonEdit
    {
        [Required]
        public string? IdHoaDon { get; set; }
        public string? Phone { get; set; }
        public string? DiaChi { get; set; }
        public string? GhiChu { get; set; }
    }

}
