using ManageProduct.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProduct.DAL.Repositories
{
    public class DashboardRepository
    {
        private ManageProductContext _context;

        public int getTotalProducts()
        {
            _context = new();
            return _context.Products.Count();
        }
        public int getTotalCategories()
        {
            _context = new();
            return _context.Categories.Count();
        }
        public int getTotalBrands()
        {
            _context = new();
            return _context.Brands.Count();
        }
        public int getTotalUsers()
        {
            _context = new();
            return _context.Users.Count();
        }
        public int getTotalStocks()
        {
            _context = new();
            return _context.Products.Sum(p => p.StockQuantity);
        }
        public List<Statistic> GetTopCategories()
        {
            _context = new();
            var result = _context.Categories
                .Select(c => new Statistic
                {
                    Name = c.Name,
                    ProductCount = c.Products.Count
                })
                .OrderByDescending(c => c.ProductCount)
                .Take(5)
                .ToList();
            return result;
        }

        public List<Statistic> GetTopBrands()
        {
            _context = new();
            var result = _context.Brands
                .Select(b => new Statistic
                {
                    Name = b.Name,
                    ProductCount = b.Products.Count
                })
                .OrderByDescending(b => b.ProductCount)
                .Take(5)
                .ToList();

            return result;
        }
        public List<Statistic> GetTopRoles()
        {
            _context = new();
            var result = _context.Roles
                .Select(r => new Statistic
                {
                    Name = r.RoleName,
                    ProductCount = r.Users.Count
                })
                .OrderByDescending(r => r.ProductCount)
                .Take(5)
                .ToList();
            return result;
        }


        public decimal GetAveragePrice()
        {
            _context = new();
            return _context.Products.Average(p => p.Price);
        }
        public decimal GetMinPrice()
        {
            _context = new();
            return _context.Products.Min(p => p.Price);
        }
        public decimal GetMaxPrice()
        {
            _context = new();
            return _context.Products.Max(p => p.Price);
        }
        public decimal GetTotalInventoryValue()
        {
            _context = new();
            return _context.Products.Sum(p => p.Price * p.StockQuantity);
        }
    }
}
