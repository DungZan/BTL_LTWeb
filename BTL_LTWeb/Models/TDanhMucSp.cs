using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.Models;

public partial class TDanhMucSp
{
    public int MaSp { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    public string? TenSp { get; set; }

    [Required(ErrorMessage = "Chất liệu không được để trống")]
    public string? ChatLieu { get; set; }

    [Required(ErrorMessage = "Loại không được để trống")]
    public string? LoaiDt { get; set; }

    [Required(ErrorMessage = "Hãng sản xuất không được để trống")]
    public string? HangSx { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Thời gian bảo hành phải là số không âm")]
    public int? ThoiGianBaoHanh { get; set; }

    public string? GioiThieuSp { get; set; }

    public string? AnhDaiDien { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải là số không âm")]
    public decimal? Gia { get; set; }

    public virtual ICollection<TAnhSp> TAnhSps { get; set; } = new List<TAnhSp>();

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}
