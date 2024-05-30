using System;
using System.Collections.Generic;

namespace ShopTMDT.Data;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? RoleId { get; set; }

    public string? FullName { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? Anh { get; set; }

    public int? Diem { get; set; }

    public string? Qrcode { get; set; }

    public virtual ICollection<NhapHangHoa> NhapHangHoas { get; set; } = new List<NhapHangHoa>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<XuatHangHoa> XuatHangHoas { get; set; } = new List<XuatHangHoa>();
}
