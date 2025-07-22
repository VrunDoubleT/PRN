using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;

namespace ManageProduct.BLL.Services
{
    public class StaffService
    {
        private readonly StaffRepository _staffRepository;

        public StaffService(StaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public List<User> GetStaffs()
        {
            return _staffRepository.GetStaffs();
        }

        public void AddStaff(string fullName, string email)
        {
            _staffRepository.AddStaff(fullName, email);
        }

        public void UpdateStaff(int userId, string fullName, string email)
        {
            _staffRepository.UpdateStaff(userId, fullName, email);
        }

        public void DeleteStaff(int userId)
        {
            _staffRepository.DeleteStaff(userId);
        }
    }
}
