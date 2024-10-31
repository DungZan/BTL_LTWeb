namespace BTL_LTWeb.ViewModels
{
    public class PaymentViewModel
    {
        public int MaKhachHang { get; set; } // Mã khách hàng
        public string PhuongThucThanhToan { get; set; } // Phương thức thanh toán (Ví dụ: Thẻ tín dụng, Tiền mặt, ...)

        // Thông tin khách hàng
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string ThanhPho { get; set; }
        public string QuanHuyen { get; set; }
        public string SDT { get; set; }
        public string GhiChu { get; set; }

        // Thông tin giao hàng ở địa chỉ khác
        public int GiaoHangDiaChiKhac { get; set; } // Checkbox để chọn giao hàng ở địa chỉ khác
        public string DiaChiKhac { get; set; }
        public string ThanhPhoKhac { get; set; }
        public string QuanHuyenKhac { get; set; }
        public string SDTKhac { get; set; }
        public string HoTenKhac { get; set; }

        public List<int> CartID { get; set; }

        public string DiscountCodes { get; set; } 

    }

}
