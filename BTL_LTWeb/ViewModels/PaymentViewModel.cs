using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.ViewModels
{
    public class PaymentViewModel
    {
        public int MaKhachHang { get; set; } 
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PhuongThucThanhToan { get; set; } 

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thành phố")]
        public string ThanhPho { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập quận/huyện")]
        public string QuanHuyen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SDT { get; set; }
        public string GhiChu { get; set; }

        // Thông tin giao hàng ở địa chỉ khác
        public int GiaoHangDiaChiKhac { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(200, ErrorMessage = "Địa chỉ khác không được vượt quá 200 ký tự.")]
        public string DiaChiKhac { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thành phố")]
        [StringLength(100, ErrorMessage = "Thành phố khác không được vượt quá 100 ký tự.")]
        public string ThanhPhoKhac { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn quận/huyện")]
        [StringLength(100, ErrorMessage = "Quận/huyện khác không được vượt quá 100 ký tự.")]
        public string QuanHuyenKhac { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại khác không hợp lệ.")]
        public string SDTKhac { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên khác không được vượt quá 100 ký tự.")]
        public string HoTenKhac { get; set; }

        public List<int> CartID { get; set; }

    }

}
