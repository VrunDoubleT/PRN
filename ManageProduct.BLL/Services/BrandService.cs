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

        public bool AddBrand(string brandName)
        {
            if (_brandRepository.BrandNameExists(brandName))
                return false;

            _brandRepository.AddBrand(brandName);
            return true;
        }

        public bool UpdateBrand(int brandId, string brandName)
        {
            if (_brandRepository.BrandNameExists(brandName, brandId))
                return false;

            _brandRepository.UpdateBrand(brandId, brandName);
            return true;
        }

        public bool DeleteBrand(int brandId)
        {
            return _brandRepository.DeleteBrand(brandId);
        }
    }
}
