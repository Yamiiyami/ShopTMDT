using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class LoaiHangHoa
{
    public int IdLoaiHangHoa { get; set; }

    public string? TenLoai { get; set; }

    public int? IdKhuyenMai { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();

    public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }
}
