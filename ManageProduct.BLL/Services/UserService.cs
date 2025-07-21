using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;

namespace ManageProduct.BLL.Services
{
    public class UserService
    {
        private readonly UserRepositories _repo = new UserRepositories();

        public User? Login(string email, string password)
        {
            return _repo.CheckLogin(email, password);
        }
    }
}
  