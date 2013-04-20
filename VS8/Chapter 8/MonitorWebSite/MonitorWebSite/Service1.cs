using System;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using System.Net;

namespace MonitorWebSite
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            timer1.Interval = 10000;
        }

        protected override void OnStart(string[] args)
        {
            timer1.Start();
        }

        protected override void OnStop()
        {
            timer1.Stop();
        }

        protected override void OnContinue()
        {
            timer1.Start();
        }

        protected override void OnPause()
        {
            timer1.Stop();
        }

        protected override void OnShutdown()
        {
            timer1.Stop();
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string url = @"http://www.microsoft.com/";
                HttpWebRequest g = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse r = (HttpWebResponse)g.GetResponse();

                string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log.txt";

                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine(DateTime.Now.ToString() + " for " + url + ": " + r.StatusCode.ToString());
                tw.Close();
                r.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Exception: " + ex.Message.ToString());
            }
        }
    }
}
