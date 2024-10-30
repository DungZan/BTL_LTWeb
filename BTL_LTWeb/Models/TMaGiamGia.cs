using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BTL_LTWeb.Models
{
    public partial class TMaGiamGia
    {
        public int MaGiamGia { get; set; }
        [Required(ErrorMessage = "Mã giảm giá không được để trống")]
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Tỉ lệ giảm không được để trống")]
        [Display(Name = "Tỉ lệ giảm")]
        [Range(0, 100, ErrorMessage = "Giá trị giảm phải từ 0 đến 1.000")]
        public decimal TiLeGiam { get; set; }

        [Display(Name = "Mô tả")]
        public string? Mota {  get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime? NgayBatDau { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime? NgayKetThuc {  get; set; }
        public int? TrangThai { get; set; }

        [Display(Name = "Loại giảm giá")]
        public int? LoaiGiamGia { get; set; }

        public virtual ICollection<TMaGiamGiaSanPham> TMaGiamGiaSanPhams { get; set; } = new List<TMaGiamGiaSanPham>();
    }
}
