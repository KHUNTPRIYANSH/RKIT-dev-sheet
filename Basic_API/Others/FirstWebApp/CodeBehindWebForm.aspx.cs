﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstWebApp
{
    public partial class CodeBehindWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void hitMeButton_Click(object sender, EventArgs e)
        {
            Response.Write("hit me button clicked by the user in the site");
        }
    }
}