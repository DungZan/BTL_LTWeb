using System;
using System.Collections.Generic;

namespace BTL_LTWeb.Models;

public partial class THoaDonBan
{
    public int MaSp { get; set; }

    public DateOnly? NgayHoaDon { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaNhanVien { get; set; }

    public decimal? TongTienHd { get; set; }

    public decimal? DonGiaBan { get; set; }

    public int? SoLuongBan { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public string? GhiChu { get; set; }

    public virtual TKhachHang? MaKhachHangNavigation { get; set; }

    public virtual TNhanVien? MaNhanVienNavigation { get; set; }

    public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;
}
