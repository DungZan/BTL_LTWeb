using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWeb.Models
{
    public class TGioHang
    {
        public int MaGioHang { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; }

        [ForeignKey("ChiTietSanPham")]
        public int MaChiTietSP { get; set; }
        public int SoLuong { get; set; }

        public virtual TUser User { get; set; } = null!;
        public virtual TChiTietSanPham ChiTietSanPham { get; set; }
    }
}
