using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteTemplate : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	public bool NavVisible
	{
		get { return _navPanel.Visible; }
		set { _navPanel.Visible = value; }
	}
}
