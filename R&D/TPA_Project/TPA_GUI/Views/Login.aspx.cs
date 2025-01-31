using System;
using System.Web;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace TPA_GUI
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Call API to authenticate and get the token (write function separately)
            string jwtToken = AuthenticateUser(username, password);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                Session["JWTToken"] = jwtToken;  // Store token in session

                // Decode token and get dependencies
                var authModel = DecodeJwtToken(jwtToken);
                Session["Dependencies"] = authModel.Dependencies; // Store dependencies

                Response.Redirect("Dashboard.aspx"); // Redirect to dashboard
            }
            else
            {
                lblError.Text = "Invalid Credentials";
            }
        }

        private string AuthenticateUser(string username, string password)
        {
            // Call TPA_Server API (write API call logic here)
            return "JWT_TOKEN_HERE";  // Placeholder for actual token
        }

        private AuthModel DecodeJwtToken(string token)
        {
            // Decode JWT token and return AuthModel object
            return new AuthModel
            {
                Dependencies = new string[] { "dashboard", "home", "toolbox" }
            };
        }
    }

    public class AuthModel
    {
        public string[] Dependencies { get; set; }
    }
}
