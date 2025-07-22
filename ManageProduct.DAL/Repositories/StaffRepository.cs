using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;
using System.Security.Cryptography;

namespace ManageProduct.DAL.Repositories
{
    public class StaffRepository
    {
        private readonly ManageProductContext _context;

        public void SendEmail(string toEmail, string subject, string body)
        {
            string fromEmail = "nanoforge6@gmail.com";
            string password = "xejddgujfcxzhvli";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(fromEmail, password);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(mail);
            }
        }

        public static string GenerateRandomPassword(int length = 8)
        {
            if (length < 8)
                throw new ArgumentException("Password length must be at least 8 characters.");

            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            var rand = new Random();

            // Đảm bảo chứa ít nhất 1 ký tự của mỗi loại
            char[] requiredChars = new char[]
            {
            upper[rand.Next(upper.Length)],
            lower[rand.Next(lower.Length)],
            digits[rand.Next(digits.Length)],
            special[rand.Next(special.Length)]
            };

            // Tạo các ký tự còn lại
            string allChars = upper + lower + digits + special;
            int remainingLength = length - requiredChars.Length;
            var passwordChars = new char[length];

            // Gán các ký tự bắt buộc vào mật khẩu
            for (int i = 0; i < requiredChars.Length; i++)
            {
                passwordChars[i] = requiredChars[i];
            }

            // Tạo các ký tự ngẫu nhiên còn lại
            for (int i = requiredChars.Length; i < length; i++)
            {
                passwordChars[i] = allChars[rand.Next(allChars.Length)];
            }

            // Xáo trộn mật khẩu để tránh predictable pattern
            return new string(passwordChars.OrderBy(x => rand.Next()).ToArray());
        }

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

        // Add new staff
        public void AddStaff(string fullName, string email)
        {
            string password = GenerateRandomPassword();
            var staff = new User
            {
                FullName = fullName,
                Email = email,
                RoleID = 2,
                Password = HashMD5(password)
            };

            _context.Users.Add(staff);
            _context.SaveChanges();
            SendEmail(email, "Create account", "Your password: " + password);
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

        public void ResetStaffPassword(int userId)
        {
            var staff = _context.Users.FirstOrDefault(u => u.UserID == userId && u.RoleID == 2);
            if (staff != null)
            {
                string newPassword = GenerateRandomPassword(10);
                staff.Password = HashMD5(newPassword);
                _context.SaveChanges();
                SendEmail(staff.Email, "Create account", "New password: " + newPassword);
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

        public bool IsEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email && u.RoleID == 2);
        }

        public bool IsEmailExistsForOtherUser(int userId, string email)
        {
            return _context.Users.Any(u => u.Email == email && u.UserID != userId && u.RoleID == 2);
        }
    }
}
