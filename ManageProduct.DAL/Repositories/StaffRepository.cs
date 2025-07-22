using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;

namespace ManageProduct.DAL.Repositories
{
    public class StaffRepository
    {
        private readonly ManageProductContext _context;

        public StaffRepository(ManageProductContext context)
        {
            _context = context;
        }

        // Get staff list
        public List<User> GetStaffs()
        {
            return _context.Users
                .Where(u => u.RoleID == 2)
                .Select(u => new User
                {
                    UserID = u.UserID,
                    FullName = u.FullName,
                    Email = u.Email
                }).ToList();
        }

        // Add new staff
        public void AddStaff(string fullName, string email)
        {
            var tempPassword = "temp_password";
            var staff = new User
            {
                FullName = fullName,
                Email = email,
                RoleID = 2,
                Password = tempPassword
            };

            _context.Users.Add(staff);
            _context.SaveChanges();
            // Password formatted hashed_password_[id]
            staff.Password = $"hashed_password_{staff.UserID}";
            _context.SaveChanges();
        }


        // Update staff
        public void UpdateStaff(int userId, string fullName, string email)
        {
            var staff = _context.Users.FirstOrDefault(u => u.UserID == userId && u.RoleID == 2);
            if (staff != null)
            {
                staff.FullName = fullName;
                staff.Email = email;
                _context.SaveChanges();
            }
        }

        // Delete staff
        public void DeleteStaff(int userId)
        {
            var staff = _context.Users.FirstOrDefault(u => u.UserID == userId && u.RoleID == 2);
            if (staff != null)
            {
                _context.Users.Remove(staff);
                _context.SaveChanges();
            }
        }
    }
}
