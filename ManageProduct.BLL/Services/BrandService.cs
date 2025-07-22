using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProduct.BLL.Services
{
    public class BrandService
    {
        private readonly BrandRepository _brandRepository;

        public BrandService(BrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public List<Brand> GetBrands()
        {
            return _brandRepository.GetBrands();
        }

        public void AddBrand(string brandName)
        {
            _brandRepository.AddBrand(brandName);
        }

        public void UpdateBrand(int brandId, string brandName)
        {
            _brandRepository.UpdateBrand(brandId, brandName);
        }

        public void DeleteBrand(int brandId)
        {
            _brandRepository.DeleteBrand(brandId);
        }
    }
}
