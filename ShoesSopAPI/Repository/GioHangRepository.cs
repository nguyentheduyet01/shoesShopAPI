using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Repository.Interface;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace ShoesSopAPI.Repository

{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly DBShop _dBShop;
        public GioHangRepository(DBShop dBShop)
        {
            _dBShop = dBShop;
        }

        public async Task<GioHang> DeleteGioHang(int id, int userId)
        {
            GioHang gioHang = _dBShop.GioHangs.Where(n => n.SanPhamId == id && n.KhachHangId == userId).FirstOrDefault();
            if (gioHang == null)
            {
                return null;
            }
            using (IDbContextTransaction transaction = _dBShop.Database.BeginTransaction())
            {
                try
                {
                    _dBShop.GioHangs.Remove(gioHang);
                    _ = _dBShop.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }

            return gioHang;
        }

        public async Task<GioHang> EditGioHang(int id, GioHang gioHang)
        {
            using (IDbContextTransaction transaction = _dBShop.Database.BeginTransaction())
            {
                try
                {
                    _dBShop.Entry(gioHang).State = EntityState.Modified;
                    await _dBShop.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }

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

        public bool PostListProducToGioHang(List<int> sanPhamId, int userId)
        {
            /*  System.Data.Entity.DbContextTransaction transaction = (DbContextTransaction)_dBShop.Database.BeginTransaction();
              try
              {
                  foreach (var item in sanPhamId)
                  {
                      GioHang gioHang = new GioHang();
                      gioHang.KhachHangId = userId;
                      gioHang.SanPhamId = item;
                      _dBShop.GioHangs.AddAsync(gioHang);
                  }
                  _dBShop.SaveChangesAsync();
                  transaction.Commit();
                  return true;
              }
              catch (Exception ex)
              {
                  transaction.Rollback();
                  return false;
              }*/
            return false;
        }

        public async Task<GioHang> PostProductToGioHang(int sanPhamId, int khachHangId)
        {
            GioHang gioHang = new GioHang();
            gioHang.KhachHangId = khachHangId;
            gioHang.SanPhamId = sanPhamId;

            using (IDbContextTransaction transaction = _dBShop.Database.BeginTransaction())
            {
                try
                {
                    await _dBShop.GioHangs.AddAsync(gioHang);
                    _ = _dBShop.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }
            return gioHang;
        }
    }
}
