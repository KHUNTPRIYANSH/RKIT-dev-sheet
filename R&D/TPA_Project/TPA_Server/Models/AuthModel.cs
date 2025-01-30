using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPA_Server.Models
{
    public class AuthModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<string> Dependencies { get; set; }  // Array of functions to be called
        public int TokenExpiryInMinutes { get; set; }  // Token expiry time
    }

}