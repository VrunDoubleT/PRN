using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;

namespace ManageProduct.DAL.Repositories
{
    public class CustomerRepository
    {
        private readonly ManageProductContext _context;

        public CustomerRepository(ManageProductContext context)
        {
            _context = context;
        }

        // Get customer list
        public List<User> GetCustomers()
        {
            return _context.Users
                .Where(u => u.RoleID == 3)
                .Select(u => new User
                {
                    UserID = u.UserID,
                    FullName = u.FullName,
                    Email = u.Email
                }).ToList();
        }

        // Add new customer
        public void AddCustomer(string fullName, string email)
        {
            var tempPassword = "temp_password";
            var customer = new User
            {
                FullName = fullName,
                Email = email,
                RoleID = 3,
                Password = tempPassword
            };

            _context.Users.Add(customer);
            _context.SaveChanges();
            // Password formatted hashed_password_[id]
            customer.Password = $"hashed_password_{customer.UserID}";
            _context.SaveChanges();
        }


        // Update customer
        public void UpdateCustomer(int userId, string fullName, string email)
        {
            var customer = _context.Users.FirstOrDefault(u => u.UserID == userId && u.RoleID == 3);
            if (customer != null)
            {
                customer.FullName = fullName;
                customer.Email = email;
                _context.SaveChanges();
            }
        }

        // Delete customer
        public void DeleteCustomer(int userId)
        {
            var customer = _context.Users.FirstOrDefault(u => u.UserID == userId && u.RoleID == 3);
            if (customer != null)
            {
                _context.Users.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
