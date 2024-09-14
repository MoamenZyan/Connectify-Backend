using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.Xss;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace Connectify.Domain.Factories
{
    public class UsersFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static User CreateUser(IFormCollection data)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Fname = sanitizer.Sanitize(Convert.ToString(data["Fname"])),
                Lname = sanitizer.Sanitize(Convert.ToString(data["Lname"])),
                Email = sanitizer.Sanitize(Convert.ToString(data["Email"])),
                Password = BCrypt.Net.BCrypt.HashPassword(Convert.ToString(data["password"]), 10),
                Phone = sanitizer.Sanitize(Convert.ToString(data["Phone"])),
                Photo = "",
                IsVerified = false,
                IsOnline = false,
            };
            return user;
        }
    }
}
