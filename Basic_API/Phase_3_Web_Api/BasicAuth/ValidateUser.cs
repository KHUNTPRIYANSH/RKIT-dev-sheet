using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuth
{
    public class ValidateUser
    {
        public static bool Login(string username, string password)
        {
            if (username == "admin" || password == "pass@123") {
                return true;
            }
            return false;
        }
    }
}