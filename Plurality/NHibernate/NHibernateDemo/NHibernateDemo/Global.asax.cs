using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Dialect;
using System.Reflection;
using NHibernate;
using NHibernateDemo.Code;
using System.IO;

namespace NHibernateDemo
{
    public class Global : System.Web.HttpApplication
    {
        public static ISessionFactory SessionFactory;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            var cfg = new Configuration();

            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=NHibernateDemo;Integrated Security=True";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                x.LogSqlInConsole = true;
            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            SessionFactory = cfg.BuildSessionFactory();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
