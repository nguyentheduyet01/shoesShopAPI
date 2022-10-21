﻿using ShoesSopAPI.Models;

namespace ShoesSopAPI.Repository.Interface
{
    public interface IGioHangRepository
    {
        Task<IEnumerable<GioHang>> GetListAllGioHang();
        Task<IEnumerable<SanPham>> GetListByCustomerId(string id);
        Task<GioHang> PostProductToGioHang(int id, int khachhangid);
        Task<GioHang> DeleteGioHang(int id, int userId);
        Task<GioHang> EditGioHang(int id, GioHang gioHang);
        bool PostListProducToGioHang(List<int> sanPhamId, int userId);
    }
}
