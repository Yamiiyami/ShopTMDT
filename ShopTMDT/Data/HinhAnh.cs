using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class HinhAnh
{
    public int IdHinhAnh { get; set; }

    public string? HinhAnh1 { get; set; }

    public string? TenHinhAnh { get; set; }

    public int? IdHangHoa { get; set; }

    public virtual HangHoa? IdHangHoaNavigation { get; set; }
}
