using System;
using System.Collections.Generic;

namespace BTL_LTWeb.Models;

public partial class TChiTietSanPham
{
    public int MaChiTietSp { get; set; }

    public int MaSp { get; set; }

    public string? KichThuoc { get; set; }

    public string? MauSac { get; set; }

    public string? AnhDaiDien { get; set; }

    public decimal? DonGiaBan { get; set; }

    public int? Slton { get; set; }

    public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;

    public virtual ICollection<TAnhChiTietSp> TAnhChiTietSps { get; set; } = new List<TAnhChiTietSp>();
}
