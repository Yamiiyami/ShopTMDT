using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class NhaCungCap
{
    public int IdNhaCungCap { get; set; }

    public string? Ten { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public DateTime? NgayHopTac { get; set; }

    public virtual ICollection<NhapHangHoa> NhapHangHoas { get; set; } = new List<NhapHangHoa>();
}
