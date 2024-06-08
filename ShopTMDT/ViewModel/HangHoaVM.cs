using Microsoft.EntityFrameworkCore;
using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class HangHoaVM
    {

        public string? MauSac { get; set; }

        public int? SoLuong { get; set; }

        public string? Size { get; set; }

        public string? HinhAnh { get; set; }

        public string? TenHangHoa { get; set; }

        public decimal? Gia { get; set; }

        public int? TongRating { get; set; }

        public int? TongSao { get; set; }

        public string? MoTa { get; set; }

        public int? IdKhuyenMai { get; set; }

        public int? IdLoaiHangHoa { get; set; }

        public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }

        public virtual LoaiHangHoa? IdLoaiHangHoaNavigation { get; set; }


    }
    public class HangHoaMD : HangHoaVM
    {
        public int IdHangHoa { get; set; }
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; } = new List<HinhAnh>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<ThongTinDonNhap> ThongTinDonNhaps { get; set; } = new List<ThongTinDonNhap>();
        public virtual ICollection<ThongTinXuat> ThongTinXuats { get; set; } = new List<ThongTinXuat>();

    }

    public class HangHoaRequest
    {
        public string? MauSac { get; set; }

        public int? SoLuong { get; set; }

        public string? Size { get; set; }

        public string? HinhAnh { get; set; }

        public string? TenHangHoa { get; set; }

        public decimal? Gia { get; set; }

        public int? TongRating { get; set; }

        public int? TongSao { get; set; }

        public string? MoTa { get; set; }

        public int? IdKhuyenMai { get; set; }

        public int? IdLoaiHangHoa { get; set; }

    }

}
