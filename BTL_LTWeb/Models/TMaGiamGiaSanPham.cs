namespace BTL_LTWeb.Models
{
    public class TMaGiamGiaSanPham
    {
        public int MaGiamGia { get; set; }
        public int MaSp { get; set; }

        public virtual TMaGiamGia MaGiamGiaNavigation { get; set; }
        public virtual TDanhMucSp MaSpNavigation { get; set; }
    }
}
