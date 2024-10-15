using Microsoft.AspNetCore.Identity;

namespace BTL_LTWeb.Models;

public partial class TUser : IdentityUser
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? LoaiUser { get; set; }
    public string Salt { get; set; } = null!;   

    public virtual ICollection<TKhachHang> TKhachHangs { get; set; } = new List<TKhachHang>();

    public virtual ICollection<TNhanVien> TNhanViens { get; set; } = new List<TNhanVien>();
}
