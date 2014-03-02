#define TRACE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["MySessionState"] = "This is some session state.";
            Application["MyApplicationState"] = "This is some application state.";
            Response.Cookies.Add(new HttpCookie("My Cookie", "This is some cookie state."));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = TextBox1.Text;

            System.Diagnostics.Trace.WriteLine("This is from S.D.Tracd", "My Category");

            Trace.Write("My Category", "Trace messages are ignored unless tracing is enabled");

            if (Trace.IsEnabled)
            {
                Trace.Write("My Category", "You can add additional testing code that... cookies!");
            }

            Trace.Warn("My Category", "This is from Trace.Warn.");
        }
    }
}
