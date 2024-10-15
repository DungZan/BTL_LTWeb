using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace BTL_LTWeb.Models;

public partial class TKhachHang
{
    public int MaKhachHang { get; set; }

    public string Username { get; set; } = null!;

    public string? TenKhachHang { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? GhiChu { get; set; }
    [ValidateNever]
    public virtual TUser UsernameNavigation { get; set; } = null!;
}
