using Microsoft.AspNetCore.Mvc;
using ShoesSopAPI.Data;
using ShoesSopAPI.DTO;
using ShoesSopAPI.Models;
using ShoesSopAPI.Services.Interfaces;

namespace ShoesSopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangsController : ControllerBase
    {
        private readonly IGioHangsService _gioHangsService;
        private readonly DBShop _dBShop;
        public GioHangsController(IGioHangsService gioHangsService)
        {
            _gioHangsService = gioHangsService;
            _dBShop = new DBShop();
        }

        // GET: api/GioHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHangDto>>> GetGioHangs()
        {
            var list =  await _gioHangsService.GetListAllGioHang();
            if(list == null)
            {
                return NoContent();
            }
            else
            {
                var listGioHangDto =  list.Select(n => new GioHangDto
                {
                    Id = n.Id,
                    SanPhamId = n.SanPhamId,
                    KhachHangId = n.KhachHangId
                });
                return Ok(listGioHangDto);
            }
        }

        // GET: api/GioHangs/id khach hang 
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SanPhamDto>>> GetGioHang(string id)
        {
            var listProduct =  await _gioHangsService.GetListByCustomerId(id);
            if (listProduct == null)
                return NoContent();
            else
            {
                var listProductDto = listProduct.Select(n => new SanPhamDto
                {
                    Id = n.Id,
                    TenSanPham = n.TenSanPham,
                    MoTa = n.MoTa,
                    Anh = n.Anh,
                    CreatedbyId = n.CreatedbyId,
                    Gia = n.Gia,
                    Sale = n.Sale,
                    Loai = n.Loai,
                    NgayTao = n.NgayTao
                });
                return Ok(listProductDto);
            }
        }

        /*[Authorize]
        [HttpPut("{id}")]*/
        /*  public async Task<IActionResult> PutGioHang(int id, GioHang gioHang)
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
          }*/

        // POST: api/GioHangs
        [HttpPost]
        public async Task<ActionResult> PostGioHang(int sanPhamId, int khachHangId)
        {
           var result = await _gioHangsService.PostProductToGioHang(sanPhamId, khachHangId);
            if (result == null)
                return BadRequest("Them san pham khong thanh cong");
            else
            {
                return Ok();
            }
        }

        // DELETE: api/GioHangs/ id san pham 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGioHang(int id)
        {
            GioHang result = await _gioHangsService.DeleteGioHang(id);
            if (result == null)
                return BadRequest("Xoa san pham khong thanh cong");
            else
            {
                return Ok();
            }
        }

    }
}
