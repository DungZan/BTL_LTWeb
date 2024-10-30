namespace BTL_LTWeb.Models
{
    public partial class TMaGiamGia
    {
        public int MaGiamGia { get; set; }
        public string Code { get; set; }
        public decimal TiLeGiam { get; set; }
        public string? Mota {  get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc {  get; set; }
        public int? TrangThai { get; set; }

        public int? LoaiGiamGia { get; set; }

        public virtual ICollection<TMaGiamGiaSanPham> TMaGiamGiaSanPhams { get; set; } = new List<TMaGiamGiaSanPham>();
    }
}
