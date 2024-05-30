using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class TrangThaiVanTruyen
{
    public string IdVanTruyen { get; set; } = null!;

    public string? StatusName { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<XuatHangHoa> XuatHangHoas { get; set; } = new List<XuatHangHoa>();
}
