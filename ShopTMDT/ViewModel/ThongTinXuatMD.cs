using ShopTMDT.Data;

namespace ShopTMDT.ViewModel
{
    public class ThongTinXuatVM
    {

        public string? IdXuatHangHoa { get; set; }

        public int? IdHangHoa { get; set; }

        public int? IdKhuyenMai { get; set; }

        public int? SoLuong { get; set; }

        public decimal? Gia { get; set; }

        public decimal? TongGia { get; set; }

    }
    public class ThongTinXuatMD : ThongTinXuatVM
    {
        public int IdThongTinXuat { get; set; }

        public virtual HangHoa? IdHangHoaNavigation { get; set; }

        public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }

        public virtual XuatHangHoa? IdXuatHangHoaNavigation { get; set; }
    }
    public class ThongTinXuatResponse
    {
        public string? IdXuatHangHoa { get; set; }

        public int? IdHangHoa { get; set; }

        public int? IdKhuyenMai { get; set; }

        public int? SoLuong { get; set; }

        public decimal? Gia { get; set; }

        public decimal? TongGia { get; set; }

    }
}
