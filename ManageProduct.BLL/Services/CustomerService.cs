using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;

namespace ManageProduct.BLL.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<User> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public void AddCustomer(string fullName, string email)
        {
            _customerRepository.AddCustomer(fullName, email);
        }

        public void UpdateCustomer(int userId, string fullName, string email)
        {
            _customerRepository.UpdateCustomer(userId, fullName, email);
        }

        public void DeleteCustomer(int userId)
        {
            _customerRepository.DeleteCustomer(userId);
        }
    }
}
