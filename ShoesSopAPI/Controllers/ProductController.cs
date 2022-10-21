using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesSopAPI.DTO;
using ShoesSopAPI.Models;
using ShoesSopAPI.Services.Interfaces;

namespace ShoesSopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPhamDto>>> GetSanPham(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            var listProduct =  await _productService.GetAllProduct(search,from, to, sortBy, page);
            if(listProduct == null)
            {
                return BadRequest();
            }
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

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetSanPham(int id)
        {
            var sanPham = await _productService.GetProductById(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return sanPham;
        }
        // danh sach san pham theo loai
        [Route("loai")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPhamDto>>> GetLoaiSanPhams(int id)
        {
            var list = await _productService.GetListProductByType(id);
            if(list == null)
            {
                return NotFound();
            }
            var listProductDto = list.Select(n => new SanPhamDto
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
       
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, SanPham sanPham)
        {
            if (id != sanPham.Id)
            {
                return BadRequest();
            }
            var result = await _productService.PutProduct(id, sanPham);
            if(ReferenceEquals(result, false))
            {
                return BadRequest("Sửa không thành công");
            }
            return Ok("Sửa thành công");
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<SanPhamDto>> PostSanPham(SanPham sanPham)
        {
            var product = await _productService.PostProduct(sanPham);
            if (sanPham == null)
            {
                return NotFound();
            }

            return new SanPhamDto
            {
                Id = product.Id,
                TenSanPham = product.TenSanPham,
                MoTa = product.MoTa,
                Anh = product.Anh,
                CreatedbyId = product.CreatedbyId,
                Gia = product.Gia,
                Sale = product.Sale,
                Loai = product.Loai,
                NgayTao = product.NgayTao
            };
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            var sanPham =  _productService.DeleteProduct(id);
            if (sanPham == false)
            {
                return BadRequest("Xóa không thành công");
            }
            return NoContent();
        }

    }
}
