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
using System.Collections;
using System.IO;
using System.Configuration.Install;
using System.ComponentModel;

namespace HelloProject
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
    }

    [RunInstaller(true)]
    public class HelloInstaller : Installer
    {
        public HelloInstaller()
            : base()
        {
        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
            File.CreateText("Install.txt");
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            File.CreateText("Commit.txt");
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            File.Delete("Install.txt");
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            File.Delete("Install.txt");
            File.Delete("Commit.txt");
        }
    }
}
