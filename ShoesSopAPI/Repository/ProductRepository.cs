using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Repository.Interface;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace ShoesSopAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBShop _dBShop;
        public static int PAGE_SIZE { get; set; } = 9;
        public ProductRepository(DBShop dBShop)
        {
            _dBShop = dBShop;
        }

        public bool DeleteProduct(int id)
        {
            SanPham sanPham = _dBShop.SanPhams.FirstOrDefault(n => n.Id == id);
            if (sanPham == null)
                return false;
            else
            {
                using (IDbContextTransaction transaction = _dBShop.Database.BeginTransaction())
                {
                    try
                    {
                        _dBShop.SanPhams.Remove(sanPham);
                        _dBShop.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
               
                return true;
            }
        }

        public async Task<IEnumerable<SanPham>> GetAllProduct(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            var allProducts = _dBShop.SanPhams.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(sp => sp.TenSanPham.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(sp => sp.Gia >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(sp => sp.Gia <= to);
            }
            #endregion


            #region Sorting
            //Default sort by Name (Tensp)
            allProducts = allProducts.OrderBy(sp => sp.TenSanPham);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tensp_desc": allProducts = allProducts.OrderByDescending(sp => sp.TenSanPham); break;
                    case "gia_asc": allProducts = allProducts.OrderBy(sp => sp.Gia); break;
                    case "gia_desc": allProducts = allProducts.OrderByDescending(sp => sp.Gia); break;
                    case "ngay_desc": allProducts = allProducts.OrderByDescending(sp => sp.NgayTao); break;
                    case "sale_asc": allProducts = allProducts.OrderBy(sp => sp.Sale); break;
                    case "sale_desc": allProducts = allProducts.OrderByDescending(sp => sp.Sale); break;
                    default: allProducts = allProducts.OrderBy(sp => sp.TenSanPham); break;
                }
            }
            #endregion
            var result = PaginatedList<SanPham>.Create(allProducts, page, PAGE_SIZE);

            return result.ToList();
        }

        public async Task<IEnumerable<SanPham>> GetListProductByType(int type)
        {
            return await _dBShop.SanPhams.Where(n => n.Loai == type).ToListAsync();
        }

        public async Task<SanPham> GetProductById(int id)
        {
            return await _dBShop.SanPhams.FindAsync(id);
        }

        public async Task<SanPham> PostProduct(SanPham product)
        {
            using (IDbContextTransaction transaction = _dBShop.Database.BeginTransaction())
            {
                try
                {
                    _dBShop.SanPhams.Add(product);
                    await _dBShop.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }
            return product;
        }

        public async Task<bool> PutProduct(int id, SanPham product)
        {

            using (IDbContextTransaction transaction = _dBShop.Database.BeginTransaction())
            {
                try
                {
                    _dBShop.Entry(product).State = EntityState.Modified;
                    await _dBShop.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }
    }
}
