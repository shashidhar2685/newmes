using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newmes.WebAPI.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
