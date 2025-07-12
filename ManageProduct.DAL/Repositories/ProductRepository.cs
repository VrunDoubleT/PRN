using ManageProduct.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProduct.DAL.Repositories
{
    public class ProductRepository
    {
        private ManageProductContext _context;

        public List<Product> GetProducts()
        {
            _context = new ManageProductContext();
            return _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .ToList();
        }

        public bool CreateProduct(Product product)
        {
            try
            {
                using var _context = new ManageProductContext();

                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating product: " + ex.Message);
                return false;
            }
        }

        public void DeleteProduct(int id)
        {
            using var context = new ManageProductContext();

            // Tìm sản phẩm cần xóa theo ID
            var product = _context.Products
                                 .Include(p => p.Brand)
                                 .Include(p => p.Category)
                                 .FirstOrDefault(p => p.ProductID == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
