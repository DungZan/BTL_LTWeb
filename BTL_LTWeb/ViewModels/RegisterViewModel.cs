using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa, 1 chữ thường, 1 số, và 1 ký tự đặc biệt.")]
        public string Password { get; set; }
    }
}
