using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class ThongTinXuat
{
    public int IdThongTinXuat { get; set; }

    public string? IdXuatHangHoa { get; set; }

    public int? IdHangHoa { get; set; }

    public int? IdKhuyenMai { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public decimal? TongGia { get; set; }

    public virtual HangHoa? IdHangHoaNavigation { get; set; }

    public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }

    public virtual XuatHangHoa? IdXuatHangHoaNavigation { get; set; }
}
