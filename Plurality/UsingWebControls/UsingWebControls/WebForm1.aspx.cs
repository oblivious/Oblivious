using System;
using System.Web.UI;

namespace UsingWebControls
{
	public partial class WebForm1 : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var ctrl = Page.LoadControl("~/UserControls/Header.ascx");
			HeaderPlaceHolder.Controls.Add(ctrl);
		}
	}
}