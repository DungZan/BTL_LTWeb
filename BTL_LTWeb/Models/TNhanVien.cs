using System;
using System.Collections.Generic;

namespace BTL_LTWeb.Models;

public partial class TNhanVien
{
    public int MaNhanVien { get; set; }

    public string Username { get; set; } = null!;

    public string? TenNhanVien { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? ChucVu { get; set; }

    public string? GhiChu { get; set; }

    public virtual TUser UsernameNavigation { get; set; } = null!;
}
