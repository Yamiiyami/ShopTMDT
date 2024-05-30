using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class XuatHangHoa
{
    public string IdHoaDon { get; set; } = null!;

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

    public virtual ICollection<ThongTinXuat> ThongTinXuats { get; set; } = new List<ThongTinXuat>();
}
