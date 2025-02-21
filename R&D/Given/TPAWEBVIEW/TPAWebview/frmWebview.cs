using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace TPAWebview
{
    public partial class frmWebview : Form
    {
        public string result = "";
        Microsoft.Web.WebView2.Core.CoreWebView2Environment WebView2Environment;
        public frmWebview(string _reqData)
        {
            InitializeComponent();            
            webView21.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
            Load += FrmWebview_Load;

        }

        private void WebView21_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                result = $"{e.InitializationException}";
                DialogResult = DialogResult.Cancel;
            }
            else
            {                
                webView21.WebMessageReceived += WebView21_WebMessageReceived;
            }
        }

        private async void FrmWebview_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(WebView2Environment);
            webView21.CoreWebView2.NavigateToString(File.ReadAllText("test.html"));            
        }

        private void WebView21_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var jsonString = e.TryGetWebMessageAsString();
            WebMessage objMsg = JsonConvert.DeserializeObject<WebMessage>(jsonString);
            if (objMsg.Action == "A001")
            {
                result = objMsg.Message;
            }
            objMsg = null;
            DialogResult = DialogResult.OK;
        }
    }

    public class WebMessage
    {
        public string Action { get; set; }
        public string Message { get; set; }
    }
}
