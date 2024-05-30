using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class ThongTinDonNhap
{
    public int IdThongTinDonNhap { get; set; }

    public string? IdNhapHangHoa { get; set; }

    public int? IdHangHoa { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public decimal? TongGia { get; set; }

    public virtual HangHoa? IdHangHoaNavigation { get; set; }

    public virtual NhapHangHoa? IdNhapHangHoaNavigation { get; set; }
}
