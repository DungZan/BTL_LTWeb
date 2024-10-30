namespace BTL_LTWeb.Models
{
    public class TMaGiamGia_LoaiSP
    {
        public int MaGiamGia { get; set; }
        public string LoaiDt { get; set; }
        public virtual TMaGiamGia MaGiamGiaNavigation { get; set; }
        public virtual TDanhMucSp LoaiDtNavigation { get; set; }
    }
}
