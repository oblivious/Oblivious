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
using System.IO;

namespace FilesFoldersTreeView
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = driveInfo.Name;
                    MessageBox.Show((string)item.Header);

                    if (driveInfo.IsReady)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(driveInfo.Name);
                        foreach (DirectoryInfo di in dirInfo.GetDirectories())
                        {
                            item.Items.Add(new TreeViewItem() { Header = di.Name });
                        }
                        foreach (FileInfo fi in dirInfo.GetFiles())
                        {
                            item.Items.Add(new TreeViewItem() { Header = fi.Name });
                        }
                    }
                    this.treeView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
