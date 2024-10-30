using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BTL_LTWeb.Controllers
{
    internal static class ReviewStaticData
    {
        internal static int _uid = 0;
        internal static string _utype = string.Empty;
        internal static int _pid = 0;
        internal static bool _hasPurchased = false;

        internal static void UpdateValue(int uid, string utype, int pid)
        {
            _uid = uid;
            _utype = utype;
            _pid = pid;
        }
    }

    public class ReviewController : Controller
    {
        private QLBanDoThoiTrangContext _db;
        private List<TDanhGia> _reviews = new List<TDanhGia>();
        private List<TPhanHoi> _reacts = new List<TPhanHoi>();

        //common account variable

        //currently in product's page
        private int _pid;

        //for pagination purpose
        private const int _PERPAGERV = 5;

        public ReviewController(QLBanDoThoiTrangContext _context)
        {
            _db = _context;
        }

        private void FetchProduct(int pid)
        {
            if (_db.TDanhMucSps.Any(it => it.MaSp == pid)) //trong trường hợp mã sp không hợp lệ
            {
                _pid = pid;
                _reviews = _db.TDanhGias.Where(it => it.MaSP == _pid).ToList();
                List<int> rids = new List<int>();
                foreach (var it in _reviews) rids.Add(it.MaDanhGia);
                _reacts = _db.TPhanHois.Where(it => rids.Contains(it.MaDanhGia)).ToList();
            }
            else
            {
                _pid = 0;
                _reviews = new List<TDanhGia>();
                _reacts = new List<TPhanHoi>();
            }
        }

        public IActionResult GlobalReviewAJAX(int pid, string email)
        {
            int _uid = 0;
            string _utype = string.Empty;
            if (!string.IsNullOrWhiteSpace(email)) //đã đăng nhập
            {
                var user = _db.TUsers.FirstOrDefault(e => e.Email == email);
                _utype = user.LoaiUser;
                switch (_utype)
                {
                    case "KhachHang":
                        _uid = _db.TKhachHangs.Where(it => it.Email == email).First().MaKhachHang;
                        break;
                    case "NhanVien":
                        _uid = _db.TNhanViens.Where(it => it.Email == email).First().MaNhanVien;
                        break;
                }
            }
            ViewBag.utype = _utype;
            ViewBag.pid = pid;

            ReviewStaticData.UpdateValue(_uid, _utype, pid);

            return PartialView("globalRV");
        }
        private IEnumerable<ReviewContentViewModel> QueryDisplableReviews()
        {
            var query = (from RV in _db.TDanhGias
                         join KH in _db.TKhachHangs on RV.MaKhachHang equals KH.MaKhachHang
                         join NV in _db.TNhanViens on RV.MaNhanVien equals NV.MaNhanVien into gj
                         from subNV in gj.DefaultIfEmpty()
                         select new ReviewContentViewModel
                         {
                             _reviewID = RV.MaDanhGia,
                             _productID = RV.MaSP,
                             _userID = RV.MaKhachHang,
                             _emplID = RV.MaNhanVien,
                             CsName = KH.TenKhachHang != null ? KH.TenKhachHang : KH.Email,
                             DatePosted = RV.NgayTao,
                             StarRated = RV.Diem,
                             RvMessage = RV.BinhLuan,
                             EpName = subNV.Email == null ? null : subNV.TenNhanVien == null ? subNV.Email : subNV.TenNhanVien,
                             RpMessage = RV.TraLoi
                         }).Where(it => !string.IsNullOrEmpty(it.RvMessage) && it._productID == _pid).ToList();
            foreach (var it in query)
            {
                List<TPhanHoi> qry = _reacts.Where(t => it._reviewID == t.MaDanhGia).ToList();
                it.VotesCasted = qry;
            }
            return query.Where(it => !string.IsNullOrEmpty(it.RvMessage) && it._productID == _pid).ToList();
        }

        public IActionResult GetStatsPV()
        {
            FetchProduct(ReviewStaticData._pid);

            return PartialView("rvStats", new ReviewStatsViewModel(_reviews));
        }
        public IActionResult GetMakerPV()
        {
            FetchProduct(ReviewStaticData._pid);

            TDanhGia userReview =
                ReviewStaticData._utype == "KhachHang" ? _reviews.Find(it => it.MaKhachHang == ReviewStaticData._uid)! : new TDanhGia();

            var dsChiTiet = _db.TChiTietSanPhams.Where(it => it.MaSp == ReviewStaticData._pid).Select(it => it.MaChiTietSp).ToList();
            var dsHoaDon = _db.THoaDonBans.Where(it => it.MaKhachHang == ReviewStaticData._uid).Select(it => it.MaHoaDonBan).ToList();
            ReviewStaticData._hasPurchased = _db.TChiTietHoaDonBans.Any(it => dsHoaDon.Contains(it.MaHoaDonBan) && dsChiTiet.Contains(it.MaChiTietSP));

            ViewBag.hasPurchased = ReviewStaticData._hasPurchased;

            return PartialView("rvMaker", userReview == null ? new TDanhGia() : userReview);
        }
        public IActionResult GetListPV(string sortType, int pageNum)
        {
            FetchProduct(ReviewStaticData._pid);

            ViewBag.utype = ReviewStaticData._utype;
            ViewBag.uid = ReviewStaticData._uid;

            IOrderedEnumerable<ReviewContentViewModel> dprvlist;
            switch (sortType)
            {
                case "sort_Stars":
                    dprvlist = QueryDisplableReviews().OrderByDescending(it => it.StarRated);
                    break;
                case "sort_Date":
                    dprvlist = QueryDisplableReviews().OrderByDescending(it => it.DatePosted);
                    break;
                case "sort_Helpful":
                    dprvlist = QueryDisplableReviews().OrderByDescending(it => it.VotesCasted.Where(it => it.HuuIch > 0).Count());
                    break;
                default:
                    dprvlist = QueryDisplableReviews().OrderByDescending(it => it.StarRated);
                    break;
            }

            ViewBag.pageCount = dprvlist.Count() / _PERPAGERV;
            if (dprvlist.Count() % _PERPAGERV > 0) ViewBag.pageCount++;

            //prevent oob values
            if (pageNum > ViewBag.pageCount) pageNum = ViewBag.pageCount;
            else if (pageNum < 1) pageNum = 1;

            //remind the view which page it has called for
            ViewBag.currentPage = pageNum == 0 ? 1 : pageNum;

            return PartialView("rvList", dprvlist.Skip((pageNum - 1) * _PERPAGERV).Take(_PERPAGERV));
        }

        private bool AuthorityCheck(string action, TDanhGia onRV)
        {
            switch (action)
            {
                case "rvCreate": return (ReviewStaticData._utype == "KhachHang" && ReviewStaticData._hasPurchased);
                case "rvEdit":
                case "rvDelete":
                    return onRV.MaKhachHang == ReviewStaticData._uid;
                case "rpCreate": return ReviewStaticData._utype == "NhanVien";
                case "rpEdit":
                case "rpDelete":
                    return onRV.MaNhanVien == ReviewStaticData._uid;

                default: return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool AlterReview([Bind("Diem,BinhLuan")] TDanhGia inpReview)
        {
            FetchProduct(ReviewStaticData._pid);

            TDanhGia userReview = _reviews.Find(it => it.MaKhachHang == ReviewStaticData._uid)!;

            //khách chưa để lại bình luận -> tạo bình luận
            if (userReview == null)
            {
                if (!AuthorityCheck("rvCreate", userReview)) return false;
                if (inpReview.Diem <= 0 || inpReview.Diem > 5) return false;

                inpReview.MaKhachHang = ReviewStaticData._uid;
                inpReview.MaSP = _pid;
                inpReview.NgayTao = DateTime.Now;

                if (ModelState.IsValid) try
                    {
                        _db.TDanhGias.Add(inpReview);
                        _db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
            }
            //khách đã để lại bình luận -> sửa bình luận
            else
            {
                if (!AuthorityCheck("rvEdit", userReview)) return false;
                if (userReview.Diem == inpReview.Diem && userReview.BinhLuan == inpReview.BinhLuan) return false;
                if (userReview.MaNhanVien != null) return false;
                if (inpReview.Diem < 1 || inpReview.Diem > 5) return false;

                //Bình luận sửa cách nhau 12 tiếng -> lưu vào LichSu
                if (DateTime.Now.Subtract(userReview.NgayTao).TotalHours > 12 && !string.IsNullOrWhiteSpace(userReview.BinhLuan))
                {
                    List<ReviewHistory> rvHistories = new List<ReviewHistory>();
                    if (!string.IsNullOrWhiteSpace(userReview.LichSu)) rvHistories = JsonSerializer.Deserialize<List<ReviewHistory>>(userReview.LichSu)!;
                    ReviewHistory recentHistory = new();
                        recentHistory.DatePosted = userReview.NgayTao;
                        recentHistory.StarRated = userReview.Diem;
                        recentHistory.Message = userReview.BinhLuan;
                        var reactList = _db.TPhanHois.Where(it => it.MaDanhGia == userReview.MaDanhGia);
                        recentHistory.LikeCount = reactList.Where(it => it.Thich > 0).Count();
                        recentHistory.HateCount = reactList.Where(it => it.Thich < 0).Count();
                    rvHistories.Add(recentHistory);
                    userReview.LichSu = JsonSerializer.Serialize(rvHistories);
                }

                userReview.NgayTao = DateTime.Now;
                userReview.Diem = inpReview.Diem;
                userReview.BinhLuan = inpReview.BinhLuan;

                return EditReview(userReview);
            }
            return true;
        }

        public IActionResult DeleteReviewAsk(int rid)
        {
            ViewBag.rid = rid;
            return PartialView("rvDeleteConfirm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteReviewConfirm(int rid)
        {
            FetchProduct(ReviewStaticData._pid);

            var review = _reviews.FirstOrDefault(it => it.MaDanhGia == rid);
            if (review != null)
            {
                if (!AuthorityCheck("rvDelete", review)) return false;
                //xoá review -> xoá react của review
                if (_reacts.Any(it => it.MaDanhGia == rid))
                    try
                    {
                        foreach (var it in _reacts.Where(it => it.MaDanhGia == rid)) _db.TPhanHois.Remove(it);
                        _db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                try
                {
                    _db.TDanhGias.Remove(review);
                    _db.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            else return false;
            return true;
        }

        private bool EditReview(TDanhGia rv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.TDanhGias.Update(rv);
                    _db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public IActionResult OpenReplySection(int rid)
        {
            ViewBag.rid = rid;
            TDanhGia queryRv = _db.TDanhGias.Find(rid)!;
            if (string.IsNullOrWhiteSpace(queryRv.TraLoi)) return PartialView("rvReplyEdit"); else return PartialView("rvReplyEdit", queryRv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool AlterReply([Bind("MaDanhGia", "TraLoi")] TDanhGia inpReply)
        {
            FetchProduct(ReviewStaticData._pid);

            if (string.IsNullOrWhiteSpace(inpReply.TraLoi)) return false;
            TDanhGia frameReview = _reviews.Find(it => it.MaDanhGia == inpReply.MaDanhGia)!;
            if (frameReview != null)
            {
                if (frameReview.MaNhanVien == null)
                {
                    if (!AuthorityCheck("rpCreate", frameReview)) return false;
                    frameReview.MaNhanVien = ReviewStaticData._uid;
                    frameReview.TraLoi = inpReply.TraLoi;
                }
                else
                {
                    if (!AuthorityCheck("rpEdit", frameReview)) return false;
                    frameReview.TraLoi = inpReply.TraLoi;
                }
                return EditReview(frameReview);
            }
            return false;
        }

        public bool DeleteReply(int rid)
        {
            FetchProduct(ReviewStaticData._pid);

            TDanhGia? frameReview = _reviews.Find(it => it.MaDanhGia == rid);
            if (frameReview != null && frameReview.MaNhanVien != null && AuthorityCheck("rpDelete", frameReview))
            {
                foreach (var it in _reacts.Where(it => it.MaDanhGia == frameReview.MaDanhGia))
                {
                    it.HuuIch = 0;
                    _db.TPhanHois.Update(it);
                }
                frameReview.MaNhanVien = null;
                frameReview.TraLoi = null;

                return EditReview(frameReview);
            }
            return false;
        }

        public bool CastVote(int rid, char type)
        {
            FetchProduct(ReviewStaticData._pid);

            if (ReviewStaticData._uid < 1 || ReviewStaticData._utype != "KhachHang") return false;

            var query = _reacts.Where(it => it.MaKhachHang == ReviewStaticData._uid && it.MaDanhGia == rid);
            TPhanHoi? castedVote = null;
            if (query.Count() > 0) castedVote = query.First();
            TDanhGia? voteatReview = _reviews.Find(it => it.MaDanhGia == rid);

            //không có review == không thể vote (=> data tempering)
            if (voteatReview == null) return false;

            //tạo vote entry cho khách khi db chưa có
            if (castedVote == null)
            {
                castedVote = new TPhanHoi();
                castedVote.MaDanhGia = rid;
                castedVote.MaKhachHang = ReviewStaticData._uid;
                castedVote.Thich = 0;
                castedVote.HuuIch = 0;
                try
                {
                    _db.TPhanHois.Add(castedVote);
                    _db.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            //cast vote theo option (đè vote = cancel out)
            switch (type)
            {
                case 'L':
                    castedVote.Thich = castedVote.Thich == 1 ? 0 : 1;
                    break;
                case 'D':
                    castedVote.Thich = castedVote.Thich == -1 ? 0 : -1;
                    break;
                case 'H':
                    if (voteatReview.MaNhanVien != null) castedVote.HuuIch = castedVote.HuuIch == 1 ? 0 : 1;
                    break;
            }

            try
            {
                _db.TPhanHois.Update(castedVote);
                _db.SaveChanges();
            }
            catch { return false; }
            return true;
        }
    }
}
