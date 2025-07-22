using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;

namespace ManageProduct.DAL.Repositories
{
    public class UserRepositories
    {
        public static class CurrentSession
        {
            public static User? CurrentUser { get; set; }
        }

        private readonly ManageProductContext _context;
       
        public UserRepositories()
        {
            _context = new ManageProductContext();
        }

        public static string HashMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

             StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public User? CheckLogin(string email, string password)
        {
            string hashedPassword = HashMD5(password);
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword && u.RoleID != 3);
        }
    }
}
