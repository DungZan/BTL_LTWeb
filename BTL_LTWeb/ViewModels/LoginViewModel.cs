using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
