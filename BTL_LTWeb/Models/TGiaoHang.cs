
namespace BTL_LTWeb.Models
{
    public class TGiaoHang
    {

        public int MaGiaoHang { get; set; }
        public int MaHoaDonBan { get; set; }
        public string ThanhPho { get; set; }
        public string QuanHuyen { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string HoTenNguoiNhan { get; set; }

        public THoaDonBan HoaDonBan { get; set; }
    }
    
}
