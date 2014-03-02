using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace DemoWAP
{
    public partial class DisplayTasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connString = WebConfigurationManager.ConnectionStrings["DemoDBConn"].ConnectionString;

            using (var conn = new SqlConnection(connString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Tasks";
                    conn.Open();
                    ListOfTasks.DataSource = cmd.ExecuteReader();
                    ListOfTasks.DataBind();
                }
            }
        }
    }
}