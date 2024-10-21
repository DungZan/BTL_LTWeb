namespace BTL_LTWeb.Models;

public partial class THoaDonBan
{
    public int MaHoaDonBan { get; set; }
    public DateTime? NgayHoaDon { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaNhanVien { get; set; }

    public decimal? TongTienHd { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public string? GhiChu { get; set; }
    public int? MaGiamGia { get; set; }
    public virtual TKhachHang? KhachHang { get; set; }

    public virtual TNhanVien? NhanVien { get; set; }
    public virtual TMaGiamGia? GiamGia { get; set; }
    public virtual ICollection<TChiTietHoaDonBan> TChiTietHoaDonBans { get; set; } = new List<TChiTietHoaDonBan>();
}
