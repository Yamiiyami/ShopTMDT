﻿
using ShopTMDT.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopTMDT.ViewModel
{
    public class ThongTinDonNhapVM
    {
        public string? IdNhapHangHoa { get; set; }

        public int? IdHangHoa { get; set; }

        public int? SoLuong { get; set; }

        public decimal? Gia { get; set; }

        public decimal? TongGia { get; set; }
    }
    public class ThongTinDonNhapMD : ThongTinDonNhapVM
    {
        public int IdThongTinDonNhap { get; set; }

        public virtual HangHoa? IdHangHoaNavigation { get; set; }

        public virtual NhapHangHoa? IdNhapHangHoaNavigation { get; set; }
    }
}
