using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UsingWebControls
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			LastNameTextBox.Focus();
		}

		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			Page.Validate();

			if (Page.IsValid)
			{
				var firstName = FirstNameTextBox.Text;
				var lastName = LastNameTextBox.Text;
				var county = CountyDropDown.SelectedValue;
				OutputLabel.Text = firstName + " " + lastName + " " + county;
			}
		}
	}
}