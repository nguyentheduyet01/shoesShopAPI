using Microsoft.EntityFrameworkCore;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Repository.Interface;
using ShoesSopAPI.Services.Interfaces;

namespace ShoesSopAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DBShop _dBShop;
        private readonly IProductRepository _productRepo;
        public ProductService(DBShop dBShop, IProductRepository productRepo)
        {
            _dBShop = dBShop;
            _productRepo = productRepo;
        }

        public bool DeleteProduct(int id)
        {
            return  _productRepo.DeleteProduct(id);
        }

        public async Task<IEnumerable<SanPham>> GetListProductByNgayTao()
        {
            return await _productRepo.GetListProductByNgayTao();
        }

        public async Task<IEnumerable<SanPham>> GetListProductBySale()
        {
            return await _productRepo.GetListProductBySale();
        }

        public async Task<IEnumerable<SanPham>> GetListProductByType(int type)
        {
            var list = await _productRepo.GetListProductByType(type);
            return list;
        }

        public async Task<SanPham> GetProductById(int id)
        {
            return await _productRepo.GetProductById(id);
        }

        public async Task<SanPham> PostProduct(SanPham product)
        {
           return await _productRepo.PostProduct(product);
        }

        public Task<bool> PutProduct(int id, SanPham product)
        {
            return _productRepo.PutProduct(id, product);
        }
    }
}
