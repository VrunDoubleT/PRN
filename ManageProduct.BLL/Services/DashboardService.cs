using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProduct.BLL.Services
{
    public class DashboardService
    {
        DashboardRepository _repo = new();

        public int getTotalProducts()
        {
            return _repo.getTotalProducts();
        }
        public int getTotalCategories()
        {
            return _repo.getTotalCategories();
        }
        public int getTotalBrands()
        {
            return _repo.getTotalBrands();
        }
        public int getTotalUsers()
        {
            return _repo.getTotalUsers();
        }
        public int getTotalStocks()
        {
            return _repo.getTotalStocks();
        }

        public List<Statistic> getTopCategories()
        {
            return _repo.GetTopCategories();
        }
        public List<Statistic> getTopBrands()
        {
            return _repo.GetTopBrands();
        }
        public decimal getAveragePrice()
        {
            return _repo.GetAveragePrice();
        }
        public List<Statistic> GetTopRoles()
        {
            return _repo.GetTopRoles();
        }

        public decimal getMinPrice()
        {
            return _repo.GetMinPrice();
        }
        public decimal getMaxPrice()
        {
            return _repo.GetMaxPrice();
        }
        public decimal getTotalInventoryValue()
        {
            return _repo.GetTotalInventoryValue();
        }
    }
}
