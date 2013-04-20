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
using System.Collections.Specialized;
using System.Configuration;

namespace RememberSettings
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("Title");
            config.AppSettings.Settings.Add("Title", titleTextBox.Text);
            config.AppSettings.Settings.Remove("Number");
            config.AppSettings.Settings.Add("Number", numberSlider.Value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ConfigurationManager.AppSettings["Title"] != null)
            {
                this.Title = ConfigurationManager.AppSettings["Title"];
                titleTextBox.Text = ConfigurationManager.AppSettings["Title"];
            }

            if (ConfigurationManager.AppSettings["Number"] != null)
            {
                numberSlider.Value = double.Parse(ConfigurationManager.AppSettings["Number"]);
            }
        }
    }
}
