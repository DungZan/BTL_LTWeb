namespace BTL_LTWeb.Models
{
    public class TMaGiamGia
    {
        public int MaGiamGia { get; set; }
        public string Code { get; set; }
        public decimal TiLeGiam { get; set; }
        public string? Mota {  get; set; }
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc {  get; set; }
        public int? TrangThai { get; set; }
    }
}
