using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesSopAPI.Data;
using ShoesSopAPI.DTO;
using ShoesSopAPI.Models;

namespace ShoesSopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangsController : ControllerBase
    {
        private readonly DBShop _context;

        public KhachHangsController(DBShop context)
        {
            _context = context;
        }

        // GET: api/KhachHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHangDto>>> GetKhachHangs()
        {
            var listKhachHang = await _context.KhachHangs.ToListAsync();
            if(listKhachHang == null)
            {
                return NotFound();
            }
            else
            {
                var listKhachHangDto = listKhachHang.Select(n => new KhachHangDto
                {
                    Id = n.Id,
                    HoTen = n.HoTen,
                    Email = n.Email,
                    GioiTinh = n.GioiTinh,
                    MatKhau = n.MatKhau,
                    Ngaysinh = n.Ngaysinh,
                    Sđt = n.Sđt
                });
                return Ok(listKhachHangDto);
            }
        }

        // GET: api/KhachHangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }
            else
            {
                KhachHangDto khachHangDto = new KhachHangDto
                {
                    Id = khachHang.Id,
                    HoTen = khachHang.HoTen,
                    Email = khachHang.Email,
                    GioiTinh = khachHang.GioiTinh,
                    MatKhau = khachHang.MatKhau,
                    Ngaysinh = khachHang.Ngaysinh,
                    Sđt = khachHang.Sđt
                };
                return Ok(khachHangDto);
            }
        }

        // PUT: api/KhachHangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.Id)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Sửa thành công");
        }

        // POST: api/KhachHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            try
            {
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetKhachHang", new { id = khachHang.Id }, khachHang);
            }
            catch
            {
                return BadRequest("Thêm khách hàng không thành công");
            }

            
        }

        // DELETE: api/KhachHangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.Id == id);
        }
    }
}
