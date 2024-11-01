using BTL_LTWeb.Models;

namespace BTL_LTWeb.ViewModels
{
    public class ChiTietDonHangViewModel
    {
        public int MaHoaDonBan { get; set; }    // Mã hóa đơn
        public DateTime NgayHoaDon { get; set; } // Ngày hóa đơn
        public string PhuongThucThanhToan { get; set; } // Phương thức thanh toán
        public decimal TongTienHd { get; set; }  // Tổng tiền hóa đơn
        public List<ProductDetailViewModel> ChiTietSanPhams { get; set; } 
    }
}
