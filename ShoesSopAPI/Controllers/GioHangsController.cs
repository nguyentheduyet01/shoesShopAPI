using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;

namespace ShoesSopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangsController : ControllerBase
    {
        private readonly DBShop _context;

        public GioHangsController(DBShop context)
        {
            _context = context;
        }

        // GET: api/GioHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHang>>> GetGioHangs()
        {
            return await _context.GioHangs.ToListAsync();
        }

        // GET: api/GioHangs/id khach hang 
        [HttpGet("{id}")]
        public async Task<IEnumerable<SanPham>> GetGioHang(string id)
        {
            var sd =  _context.GioHangs.Where(n => n.KhachHang.Sđt == id).ToList();
            var products = from p in sd
                           join c in _context.SanPhams on p.SanPhamId equals c.Id
                           select new
                           {
                               id = c.Id,
                               tensanpham = c.TenSanPham,
                               gia = c.Gia,
                               sale = c.Sale,
                               loai = c.Loai,
                               mota = c.MoTa,
                               anh = c.Anh,
                               ngaytao = c.NgayTao,
                               createby = c.CreatedbyId,
                           };
            
            List<SanPham> list = new List<SanPham>();
            foreach (var item in products)
            {
                SanPham sp = new SanPham();
                sp.Id = item.id;
                sp.TenSanPham = item.tensanpham;
                sp.Gia = item.gia;
                sp.Sale = item.sale;
                sp.Loai = item.loai;
                sp.MoTa = item.mota;
                sp.Anh = item.anh;
                sp.NgayTao = item.ngaytao;
                sp.CreatedbyId = item.createby;
                list.Add(sp);
            }
            return list;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGioHang(int id, GioHang gioHang)
        {
            if (id != gioHang.Id)
            {
                return BadRequest();
            }

            _context.Entry(gioHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GioHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GioHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GioHang>> PostGioHang(int id, int khachhangid)
        {
            GioHang gioHang = new GioHang();
            gioHang.KhachHangId = khachhangid;
            gioHang.SanPhamId = id;
            _context.GioHangs.Add(gioHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGioHang", new { id = gioHang.Id }, gioHang);
        }

        // DELETE: api/GioHangs/ id san pham 
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGioHang(int id)
        {
            var gioHang = await _context.GioHangs.Where(n => n.SanPhamId == id).FirstOrDefaultAsync();
            if (gioHang == null)
            {
                return NotFound();
            }

            _context.GioHangs.Remove(gioHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GioHangExists(int id)
        {
            return _context.GioHangs.Any(e => e.Id == id);
        }
    }
}
