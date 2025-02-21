using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TPAWebview
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string reqData = "";
            if (File.Exists("RQ_" + Program.processKey + ".txt"))
            {
                reqData = File.ReadAllText("RQ_" + Program.processKey + ".txt");
                File.Delete("RQ_" + Program.processKey + ".txt");
            }

            frmWebview frmWebview = new frmWebview(reqData);
            frmWebview.ShowDialog();

            File.WriteAllText("RS_" + Program.processKey + ".txt", frmWebview.result);
            Close();
        }
    }
}
