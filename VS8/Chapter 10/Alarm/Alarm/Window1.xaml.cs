using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;

namespace Alarm
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ManagementEventWatcher watcher = null;

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EventReceiver receiver = new EventReceiver();

            watcher = GetWatcher(new EventArrivedEventHandler(receiver.OnEventArrived));

            watcher.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            watcher.Stop();
        }

        public static ManagementEventWatcher GetWatcher(EventArrivedEventHandler handler)
        {
            WqlEventQuery query = new WqlEventQuery("__InstanceModificationEvent", new TimeSpan(0, 0, 1),
                "TargetInstance isa 'Win32_LocalTime' AND TargetInstance.Second = 0");

            ManagementEventWatcher watcher = new ManagementEventWatcher(query);

            watcher.EventArrived += new EventArrivedEventHandler(handler);

            return watcher;
        }
    }

    class EventReceiver
    {
        public void OnEventArrived(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject evt = e.NewEvent;

            string time = String.Format("{0}:{1:00}",
                ((ManagementBaseObject)evt["TargetInstance"])["Hour"],
                ((ManagementBaseObject)evt["TargetInstance"])["Minute"]);

            MessageBox.Show(time, "Current time");
        }
    }
}
