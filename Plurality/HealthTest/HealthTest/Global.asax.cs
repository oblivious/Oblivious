using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HealthTest
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Error(object sender, EventArgs e)
        {
            /*
            Exception ex = Server.GetLastError();
            
            Response.Write(ex.ToString());

            Server.ClearError();*/
        }
    }
}
