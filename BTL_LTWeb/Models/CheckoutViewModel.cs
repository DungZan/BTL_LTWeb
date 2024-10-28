namespace BTL_LTWeb.Models
{
    public class CheckoutViewModel
    {

        public IEnumerable<TGioHang> CartItems { get; set; }
        public TKhachHang CustomerInfo { get; set; }

    }
}
