namespace BTL_LTWeb.Models;

public partial class TDanhMucSp
{
    public int MaSp { get; set; }

    public string? TenSp { get; set; }

    public string? ChatLieu { get; set; }

    public string? LoaiDt { get; set; }

    public string? HangSx { get; set; }

    public int? ThoiGianBaoHanh { get; set; }

    public string? GioiThieuSp { get; set; }

    public string? AnhDaiDien { get; set; }

    public decimal? Gia { get; set; }

    public virtual ICollection<TAnhSp> TAnhSps { get; set; } = new List<TAnhSp>();

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}
