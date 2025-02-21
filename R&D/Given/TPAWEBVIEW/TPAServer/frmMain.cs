using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPAWEBVIEW
{
    public partial class frmMain : Form
    {
        static HttpListener httpListener;
        private Thread listenerThread;
        public bool can_close = false;
        private delegate void SafeCallDelegate(string text);
        public frmMain()
        {
            InitializeComponent();

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Load += FrmMain_Load;
            FormClosing += FrmMain_FormClosing;
        }



        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                httpListener = new HttpListener();
                httpListener.Prefixes.Add("http://localhost:8001/");
                httpListener.Prefixes.Add("http://127.0.0.1:8001/");
                httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

                httpListener.Start();
                this.listenerThread = new Thread(new ParameterizedThreadStart(startListener));
                listenerThread.Start();
                DisplayLogInGUI("Server started on port 8001");
            }
            catch (Exception)
            {

            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            can_close = true;
            httpListener.Stop();
        }

        public void DisplayLogInGUI(string message)
        {
            if (listBoxLog.InvokeRequired)
            {
                var d = new SafeCallDelegate(DisplayLogInGUI);
                listBoxLog.Invoke(d, new object[] { message });
            }
            else
            {
                if (listBoxLog.Lines.Length > 15)
                    listBoxLog.Clear();

                listBoxLog.Text += message + Environment.NewLine;
            }
        }

        private void startListener(object s)
        {
            while (!can_close)
            {
                //BLOCKS UNTIL A CLIENT HAS CONNECTED TO THE SERVER
                Listening();
            }
        }

        private void Listening()
        {
            try
            {
                var result = httpListener.BeginGetContext(RequestReceived, httpListener);
                result.AsyncWaitHandle.WaitOne();
            }
            catch { }
        }

        private void RequestReceived(IAsyncResult result)
        {
            if (!httpListener.IsListening)
                return;

            var context = httpListener.EndGetContext(result);
            Thread.Sleep(1000);

            if (context.Request.HttpMethod == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.AddHeader("Access-Control-Allow-Headers", "*");
                context.Response.AddHeader("Server", "Miracle Express Server");
                context.Response.Close();
                return;
            }

            var requestData = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd();

            var cleaned_requestData = System.Web.HttpUtility.UrlDecode(requestData);

            string response;

            try
            {
                string reqKey = Guid.NewGuid().ToString().Replace("-", "");

                File.WriteAllText("RQ_" + reqKey + ".txt", cleaned_requestData);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "TPAWebview.exe";
                startInfo.Arguments = reqKey;
                Process wv = new Process();
                wv.StartInfo = startInfo;
                wv.Start();
                wv.WaitForExit();

                if (File.Exists("RS_" + reqKey + ".txt"))
                {
                    response = File.ReadAllText("RS_" + reqKey + ".txt");
                    File.Delete("RS_" + reqKey + ".txt");
                }
                else
                    response= "ERROR:WV PROCESS FAILED";

            }
            catch (Exception ex)
            {
                response = "ERROR:" + ex.Message;
            }
            DisplayLogInGUI(response);
            byte[] encoded = Encoding.UTF8.GetBytes(response);
            context.Response.StatusCode = 200;
            context.Response.StatusDescription = "OK";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.AddHeader("Server", "Miracle Express Server");

            context.Response.ContentLength64 = encoded.Length;
            context.Response.OutputStream.Write(encoded, 0, encoded.Length);
            context.Response.OutputStream.Close();

            context.Response.Close();
        }
    }
}
