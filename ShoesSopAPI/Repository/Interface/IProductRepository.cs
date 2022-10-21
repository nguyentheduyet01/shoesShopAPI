﻿using ShoesSopAPI.Models;

namespace ShoesSopAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<SanPham>> GetAllProduct(string? search, double? from, double? to, string? sortBy, int page = 1);
        Task<SanPham> GetProductById(int id);
        Task<IEnumerable<SanPham>> GetListProductByType(int type);
        Task<SanPham> PostProduct(SanPham product);
        Task<bool> PutProduct(int id, SanPham product);
        Boolean DeleteProduct(int id);
    }
}
