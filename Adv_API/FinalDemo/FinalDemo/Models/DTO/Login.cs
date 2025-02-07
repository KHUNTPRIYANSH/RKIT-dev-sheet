using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.Models.DTO
{
    public class Login
    {
        /// <summary>
        /// user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// password in normal form
        /// </summary>
        public string Password { get; set; }
    }
}