namespace BTL_LTWeb.Models
{
    public class TGioHang
    {
        public string Email { get; set; }
        public int MaChiTietSP { get; set; }
        public int SoLuong { get; set; }

        public virtual TUser User { get; set; } = null!;
        public virtual TChiTietSanPham ChiTietSanPham { get; set; }
    }
}
