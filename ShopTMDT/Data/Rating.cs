using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class Rating
{
    public int IdRating { get; set; }

    public string? DanhGia { get; set; }

    public string? IdUser { get; set; }

    public int? IdHangHoa { get; set; }

    public int? SoSao { get; set; }

    public int? LuotThich { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual HangHoa? IdHangHoaNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
