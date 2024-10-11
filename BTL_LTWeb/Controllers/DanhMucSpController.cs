using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_LTWeb.Models;

namespace BTL_LTWeb.Controllers
{
    public class DanhMucSpController : Controller
    {
        private readonly QlbangHangBtlwebContext _context = new QlbangHangBtlwebContext();


        // GET: DanhMucSp
        public async Task<IActionResult> Index()
        {
            return View(await _context.TDanhMucSps.ToListAsync());
        }

        // GET: DanhMucSp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // GET: DanhMucSp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucSp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,ChatLieu,LoaiDt,HangSx,ThoiGianBaoHanh,GioiThieuSp,MaLoai,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tDanhMucSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tDanhMucSp);
        }

        // GET: DanhMucSp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }
            return View(tDanhMucSp);
        }

        // POST: DanhMucSp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,ChatLieu,LoaiDt,HangSx,ThoiGianBaoHanh,GioiThieuSp,MaLoai,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp)
        {
            if (id != tDanhMucSp.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDanhMucSp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDanhMucSpExists(tDanhMucSp.MaSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDanhMucSp);
        }

        // GET: DanhMucSp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // POST: DanhMucSp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp != null)
            {
                _context.TDanhMucSps.Remove(tDanhMucSp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDanhMucSpExists(int id)
        {
            return _context.TDanhMucSps.Any(e => e.MaSp == id);
        }
    }
}
