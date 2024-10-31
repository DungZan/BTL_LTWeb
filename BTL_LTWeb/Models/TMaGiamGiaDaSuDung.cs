namespace BTL_LTWeb.Models
{
    public class TMaGiamGiaDaSuDung
    {
        public int MaKhachHang { get; set; }
        public int MaGiamGia { get; set; }

        public virtual TMaGiamGia MaGiamGiaNavigation { get; set; }
        public virtual TKhachHang MaKhachHangNavigation { get; set; }
    }
}
