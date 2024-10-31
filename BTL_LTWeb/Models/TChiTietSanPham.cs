using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL_LTWeb.Models;

public partial class TChiTietSanPham
{
    [DisplayName("Mã Chi Tiết Sản Phẩm")]
    public int MaChiTietSp { get; set; }

    [DisplayName("Mã Sản Phẩm")]
    public int MaSp { get; set; }

    [DisplayName("Kích Thước")]
    public string? KichThuoc { get; set; }

    [DisplayName("Màu Sắc")]
    public string? MauSac { get; set; }

    [DisplayName("Ảnh Đại Diện")]
    public string? AnhDaiDien { get; set; }

    [DisplayName("Số Lượng Tồn")]
    public int? Slton { get; set; }

    public virtual TDanhMucSp DanhMucSp { get; set; } = null!;

    public virtual ICollection<TAnhChiTietSp> TAnhChiTietSps { get; set; } = new List<TAnhChiTietSp>();
}
