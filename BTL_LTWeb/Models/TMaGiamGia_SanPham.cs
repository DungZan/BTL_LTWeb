namespace BTL_LTWeb.Models
{
    public class TMaGiamGia_SanPham
    {
        public int MaGiamGia { get; set; }
        public int MaSp { get; set; }
        public virtual TMaGiamGia MaGiamGiaNavigation { get; set; }
        public virtual TDanhMucSp DanhMucSpNavigation { get; set; }
    }
}
