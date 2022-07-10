using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Models
{
    public class ApplicationUserModel 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }

    }
}
