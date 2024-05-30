using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class TrangThaiThanhToan
{
    public string IdThanhToan { get; set; } = null!;

    public string? Name { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<XuatHangHoa> XuatHangHoas { get; set; } = new List<XuatHangHoa>();
}
