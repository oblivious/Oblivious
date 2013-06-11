using System;
using System.IO;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace CASArticleQuickExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            StreamWriter myFile = new StreamWriter(@"c:\Security.txt");
            myFile.WriteLine("Trust No One");
            myFile.Close();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            StreamReader myFile = new StreamReader(@"c:\Security.txt");
            MessageBox.Show(myFile.ReadLine());
            myFile.Close();
        }

        private void btnFileRead_Click(object sender, EventArgs e)
        {
            try
            {
                InitUI(1);
            }
            catch (SecurityException se)
            {
                MessageBox.Show(se.Message, "Security Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        //[FileIOPermission(SecurityAction.Deny, Read = "C:\\")]
        private void InitUI(int uino)
        {
            // Do some initializations
            ShowUI(uino); // Call ShowUI
        }

        private void ShowUI(int uino)
        {
            switch (uino)
            {
                case 1: // That's our FileRead UI
                    ShowFileReadUI();
                    break;
                case 2:
                    // Show some other UI.
                    break;
            }
        }

        private void ShowFileReadUI()
        {
            MessageBox.Show("Before calling demand");
            FileIOPermission myPerm = new FileIOPermission(FileIOPermissionAccess.Read, "C:\\");
            myPerm.Demand();
            // All callers must have read permission to C drive.
            // Note: Using imperative syntax
            // Code to show UI
            MessageBox.Show("Showing FileRead UI");
            // This is executed only if the demand is successful.
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            try
            {
                MyOtherTestClass test = new MyOtherTestClass();
                test.MyTestMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Some error");
            }
        }

        private void someButton_Click(object sender, EventArgs e)
        {
            Evidence evidence = this.GetType().Assembly.Evidence;

            StringBuilder sb = new StringBuilder();
            foreach (var v in evidence)
            {
                sb.Append(v.ToString());
            }

            StreamWriter myFile = new StreamWriter(@"c:\Security.txt");
            myFile.WriteLine(sb.ToString());
            myFile.Close();

            ActivationContext ac = AppDomain.CurrentDomain.ActivationContext;
            if (ac == null)
            {
                MessageBox.Show("Activation Context was null");
            }
            else
            {
                ApplicationIdentity ai = ac.Identity;

                sb = new StringBuilder();

                sb.AppendLine("CodeBase: " + ai.CodeBase);
                sb.AppendLine("Full Name: " + ai.FullName);

                MessageBox.Show(sb.ToString(), "ApplicationIdentity");
            }
        }
    }

    // Try to figure out how to get Link Inheritance to work, but can't... because Inheritance Demand requirements are CRAZY...
    // This one guy said that he needed to have the base class in one assembly, the inheriting class in another assembly and
    // the calling method in a third assembly before an exception was finally thrown. It sort of makes sense if you think about
    // it: if the code is all in the same assembly, what does it matter if there's an inheritance demand, it's all your code.

    [FileIOPermission(SecurityAction.InheritanceDemand, Read = "C:\\")]
    public class MyTestClass
    {
        [FileIOPermission(SecurityAction.InheritanceDemand, Read = "C:\\")]
        public virtual void MyTestMethod()
        {
            // Do stuff!
            MessageBox.Show("I'm in the test method!");
        }
    }

    [FileIOPermission(SecurityAction.Deny, Read = "C:\\")]
    public class MyOtherTestClass : MyTestClass
    {
        [FileIOPermission(SecurityAction.Deny, Read = "C:\\")]
        public override void MyTestMethod()
        {
            MessageBox.Show("This totally shouldn't work!");
            base.MyTestMethod();
        }
    }
}
