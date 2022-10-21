using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Repository.Interface;
using ShoesSopAPI.Services.Interfaces;

namespace ShoesSopAPI.Services
{
    public class GioHangsService : IGioHangsService
    {
        private readonly DBShop _dBShop;
        private readonly IGioHangRepository _gioHangRepo;
        public GioHangsService(DBShop dBShop, IGioHangRepository gioHangRepo)
        {
            _dBShop = dBShop;
            _gioHangRepo = gioHangRepo;
        }

        public async Task<IEnumerable<SanPham>> GetListByCustomerId(string id)
        {
            return await _gioHangRepo.GetListByCustomerId(id);
        }

        public async Task<IEnumerable<GioHang>> GetListAllGioHang()
        {
            return await _gioHangRepo.GetListAllGioHang();
        }

        public async Task<GioHang> PostProductToGioHang(int sanPhamId, int khachHangId)
        {
           return await _gioHangRepo.PostProductToGioHang(sanPhamId, khachHangId);
        }

        public async Task<GioHang> DeleteGioHang(int id, int userId)
        {
            GioHang gioHang = await _gioHangRepo.DeleteGioHang(id, userId);
            return  gioHang;
        }

        public Task<GioHang> EditGioHang(int id, GioHang gioHang)
        {
            return _gioHangRepo.EditGioHang(id, gioHang);
        }

        public bool PostListProducToGioHang(List<int> sanPhamId, int userId)
        {
            return _gioHangRepo.PostListProducToGioHang(sanPhamId,userId);
        }
    }
}
