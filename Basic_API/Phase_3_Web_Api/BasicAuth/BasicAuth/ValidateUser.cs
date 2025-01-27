using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuth
{
    /// <summary>
    /// ValidateUser class is used to validate the user [username, password]
    /// </summary>
    public class ValidateUser
    {
        /// <summary>
        /// Checks the username and password 
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>bool [true,false]</returns>
        public static bool Login(string username, string password)
        {
            if (username == "admin" || password == "admin@123") {
                return true;
            }
            return false;
        }
    }
}