﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterPagesTest
{
	public partial class Default : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DateLabel.Text = DateTime.Now.ToShortDateString();
		}
	}
}