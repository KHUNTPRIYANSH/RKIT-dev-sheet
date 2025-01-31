using System;
using System.Web;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace TPA_GUI
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["JWTToken"] == null)
            {
                Response.Redirect("Login.aspx");  // Redirect to login if not authenticated
            }
        }

        public string GetDependenciesJson()
        {
            var dependencies = Session["Dependencies"] as string[];
            return new JavaScriptSerializer().Serialize(dependencies);
        }
    }
}
