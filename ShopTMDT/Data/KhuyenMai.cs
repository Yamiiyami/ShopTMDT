using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class KhuyenMai
{
    public int IdKhuyenMai { get; set; }

    public string? TenKhuyenMai { get; set; }

    public decimal? GiaKhuyenMai { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();

    public virtual ICollection<LoaiHangHoa> LoaiHangHoas { get; set; } = new List<LoaiHangHoa>();

    public virtual ICollection<ThongTinXuat> ThongTinXuats { get; set; } = new List<ThongTinXuat>();
}
