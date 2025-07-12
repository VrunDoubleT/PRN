using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProduct.BLL.Services
{
    public class ProductService
    {
        private ProductRepository _repo = new();

        public List<Product> GetProducts()
        {
            return _repo.GetProducts();
        }

        public bool CreateProduct(Product product)
        {
            return _repo.CreateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            _repo.DeleteProduct(id);
        }
    }
}
