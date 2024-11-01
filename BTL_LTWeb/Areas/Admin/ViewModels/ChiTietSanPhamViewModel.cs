using BTL_LTWeb.Models;

namespace BTL_LTWeb.ViewModels
{
    public class ChiTietSPViewModel
    {
        public TDanhMucSp Sp { get; set; }

        public List<TChiTietSanPham> chiTietSanPhams { get; set; }
    }
}
