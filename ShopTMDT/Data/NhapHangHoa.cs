using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class NhapHangHoa
{
    public string IdNhapHangHoa { get; set; } = null!;

    public string? IdUser { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? IdNhaCungCap { get; set; }

    public virtual NhaCungCap? IdNhaCungCapNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<ThongTinDonNhap> ThongTinDonNhaps { get; set; } = new List<ThongTinDonNhap>();
}
