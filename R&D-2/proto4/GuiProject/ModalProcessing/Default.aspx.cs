using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModalProcessing.DTO;
using Newtonsoft.Json;


namespace ModalProcessing
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected async void LoginButton_Click(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            using (HttpClient client = new HttpClient())
            {
                var data = new { username, password };
                string json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:58530/api/auth/login", content);
                string result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<UserDashboardDTO>(result);

                if (dto != null && !string.IsNullOrEmpty(dto.Page))
                {
                    Session["userDTO"] = dto;
                    Response.Redirect(dto.Page);
                }
                else
                {
                    Response.Write("<script>alert('Invalid login!');</script>");
                }
            }
        }
    }

}