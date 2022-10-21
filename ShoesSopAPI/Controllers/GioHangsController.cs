using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public GioHangsController(IGioHangsService gioHangsService)
        {
            _gioHangsService = gioHangsService;
        }

        // GET: api/GioHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHangDto>>> GetGioHangs()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var list = await _gioHangsService.GetListAllGioHang();
            if (list == null)
            {
                return BadRequest();
            }
            else
            {
                var listGioHangDto = list.Select(n => new GioHangDto
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
            if (id == null)
            {
                return BadRequest("error");
            }
            var listProduct = await _gioHangsService.GetListByCustomerId(id);
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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GioHangDto>> PutGioHang(int id, GioHangDto gioHang)
        {
            if (id != gioHang.Id)
            {
                return BadRequest();
            }
            GioHang gioHang1 = new GioHang
            {
                Id = gioHang.Id,
                KhachHangId = gioHang.KhachHangId,
                SanPhamId = gioHang.SanPhamId,
            };
            var result = await _gioHangsService.EditGioHang(id, gioHang1);
            if (ReferenceEquals(result, null))
            {
                return BadRequest("Sửa không thành công");
            }
            return gioHang;
        }

        // POST: api/GioHangs
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostGioHang(int sanPhamId, int khachHangId)
        {
            var result = await _gioHangsService.PostProductToGioHang(sanPhamId, khachHangId);
            if (result == null)
                return BadRequest("Them san pham vao gio hang khong thanh cong");
            else
            {
                return Ok();
            }
        }

        //POST list san pham to giohang
        /* [Authorize, HttpPost]
         public async Task<ActionResult> PostListProducToGioHang(List<int> sanPhamId, int khachHangId)
         {
             var result =  _gioHangsService.PostProductToGioHang(sanPhamId, khachHangId);
             if (ReferenceEquals(result, false))
                 return BadRequest("Them san pham vao gio hang khong thanh cong");
             else
             {
                 return Ok();
             }
         }*/
        // DELETE: api/GioHangs/ id san pham 
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGioHang(int id, int userId)
        {
            GioHang result = await _gioHangsService.DeleteGioHang(id, userId);
            if (result == null)
                return BadRequest("Xoa san pham khong thanh cong");
            else
            {
                return Ok();
            }
        }

    }
}
