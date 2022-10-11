﻿using Microsoft.EntityFrameworkCore;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Repository.Interface;

namespace ShoesSopAPI.Repository

{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly DBShop _dBShop;
        public GioHangRepository(DBShop dBShop)
        {
            _dBShop = dBShop;
        }

        public async Task<GioHang> DeleteGioHang(int id)
        {
            GioHang gioHang = _dBShop.GioHangs.Where(n => n.SanPhamId == id).FirstOrDefault();
            if (gioHang == null)
                return null;
            _dBShop.GioHangs.Remove(gioHang);
            _ = _dBShop.SaveChangesAsync();
            return gioHang;
        }

        public async Task<IEnumerable<GioHang>> GetListAllGioHang()
        {
           return await _dBShop.GioHangs.ToListAsync();
        }

        public async Task<IEnumerable<SanPham>> GetListByCustomerId(string id)
        {
            var products = from p in _dBShop.GioHangs
                           where p.KhachHang.Sđt == id
                           join c in _dBShop.SanPhams on p.SanPhamId equals c.Id
                           select c;
            return await products.ToListAsync();
        }

        public async Task<GioHang> PostProductToGioHang(int id, int sanPhamId)
        {
            GioHang gioHang = new GioHang();
            gioHang.KhachHangId = id;
            gioHang.SanPhamId = sanPhamId;
            _dBShop.GioHangs.Add(gioHang);
            await _dBShop.SaveChangesAsync();
            return gioHang;
        }
    }
}
