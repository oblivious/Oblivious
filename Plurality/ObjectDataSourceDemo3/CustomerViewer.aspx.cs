using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	protected void CustomersGridView_SelectedIndexChanged(object sender, EventArgs e)
	{
		//DetailsView1.Visible = false;
	}
	protected void UpdateProgress2_PreRender(object sender, EventArgs e)
	{

	}
	protected void DetailsView1_PreRender(object sender, EventArgs e)
	{
		//DetailsView1.Visible = true;
	}
}