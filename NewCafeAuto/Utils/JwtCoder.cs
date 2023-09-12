using Microsoft.AspNetCore.Mvc;
using NewCafeAuto.DTO.UserDTO;
using AutoMapper;
using NewCafeAuto.Models;
using System.Security.Claims;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace NewCafeAuto.Utils
{
    public class JwtCoder
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IConfiguration configuration;
        private IEnumerable<Claim> claim;

        public JwtCoder(IConfiguration configuration, NewCafeAutoContext newCafeAutoContext)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.configuration = configuration;
        }
        
        public string GenerateUniqueToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var token = new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            return token;
        }

        public string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        public string HashEmail(string email)
        {
            string hashedEmail = BCrypt.Net.BCrypt.HashString(email);
            return hashedEmail;
        }

        public void SendResetMail(string email, string resetToken)
        {
            var smtpClient = new SmtpClient("hstplanet.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(configuration["Eposta:Email"], configuration["Eposta:Password"]),
                EnableSsl = true,
            };

            var fromAddress = new MailAddress("info@hstplanet.com", "Yunus Tural");
            var toAddress = new MailAddress(email, "Yunus Tural");
            var subject = "Password Reset";
            var body = $"Click the following link to reset your password: https://gmail.com/reset?token={resetToken}";

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
            {
                smtpClient.Send(message);
            }
        }

        
    }
}
