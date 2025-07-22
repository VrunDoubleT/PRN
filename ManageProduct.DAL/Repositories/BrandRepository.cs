using ManageProduct.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProduct.DAL.Repositories
{
    public class BrandRepository
    {
        private readonly ManageProductContext _context;

        public BrandRepository(ManageProductContext context)
        {
            _context = context;
        }

        public List<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public void AddBrand(string brandName)
        {
            var brand = new Brand { Name = brandName };
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void UpdateBrand(int brandId, string brandName)
        {
            var brand = _context.Brands.Find(brandId);
            if (brand != null)
            {
                brand.Name = brandName;
                _context.SaveChanges();
            }
        }

        public void DeleteBrand(int brandId)
        {
            var brand = _context.Brands.Find(brandId);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            }
        }
    }
}
