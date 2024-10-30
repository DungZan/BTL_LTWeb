namespace BTL_LTWeb.Models
{
    public partial class TMaGiamGia
    {
        public int MaGiamGia { get; set; }
        public string Code { get; set; }
        public decimal TiLeGiam { get; set; }
        public string? Mota {  get; set; }
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc {  get; set; }
        public int? TrangThai { get; set; }

        public int? LoaiGiamGia { get; set; }

        public ICollection<TMaGiamGia_SanPham> TMaGiamGia_SanPhams { get; set; } = new List<TMaGiamGia_SanPham>();

        public ICollection<TMaGiamGia_LoaiSP> TMaGiamGia_LoaiSPs { get; set; } = new List<TMaGiamGia_LoaiSP>();
    }
}
