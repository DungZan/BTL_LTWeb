namespace BTL_LTWeb.Models
{
    public class TChiTietHoaDonBan
    {
        public int MaHoaDonBan { get; set; }
        public int MaChiTietSP { get; set; }
        public decimal? DonGiaBan { get; set; }
        public int? SoLuongBan { get; set; }

        public TDanhMucSp DanhMucSP { get; set; }
        public THoaDonBan HoaDonBan { get; set; }
        
    }
}
