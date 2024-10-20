using System.ComponentModel.DataAnnotations;

namespace BTL_LTWeb.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Mã xác nhận không được để trống.")]
        public string ConfirmationCode { get; set; }
    }
}
