using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.IO;
using System.Security.Permissions;

namespace CASDemands
{
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button declarativeDemandButton;
        private System.Windows.Forms.Button noDemandButton;
        private System.Windows.Forms.Button imperativeDemandButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.declarativeDemandButton = new System.Windows.Forms.Button();
            this.noDemandButton = new System.Windows.Forms.Button();
            this.imperativeDemandButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            this.statusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel.Location = new System.Drawing.Point(8, 48);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(272, 80);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "No action taken";
            // 
            // declarativeDemandButton
            // 
            this.declarativeDemandButton.Location = new System.Drawing.Point(8, 176);
            this.declarativeDemandButton.Name = "declarativeDemandButton";
            this.declarativeDemandButton.Size = new System.Drawing.Size(272, 23);
            this.declarativeDemandButton.TabIndex = 2;
            this.declarativeDemandButton.Text = "Create file with declarative demand";
            this.declarativeDemandButton.Click += new System.EventHandler(this.declarativeDemandButton_Click);
            // 
            // noDemandButton
            // 
            this.noDemandButton.Location = new System.Drawing.Point(8, 144);
            this.noDemandButton.Name = "noDemandButton";
            this.noDemandButton.Size = new System.Drawing.Size(272, 23);
            this.noDemandButton.TabIndex = 2;
            this.noDemandButton.Text = "Create file with no demand";
            this.noDemandButton.Click += new System.EventHandler(this.noDemandButton_Click);
            // 
            // imperativeDemandButton
            // 
            this.imperativeDemandButton.Location = new System.Drawing.Point(8, 208);
            this.imperativeDemandButton.Name = "imperativeDemandButton";
            this.imperativeDemandButton.Size = new System.Drawing.Size(272, 23);
            this.imperativeDemandButton.TabIndex = 2;
            this.imperativeDemandButton.Text = "Create file with imperative demand";
            this.imperativeDemandButton.Click += new System.EventHandler(this.imperativeDemandButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 245);
            this.Controls.Add(this.declarativeDemandButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noDemandButton);
            this.Controls.Add(this.imperativeDemandButton);
            this.Name = "Form1";
            this.Text = "Code Access Security Demands";
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void noDemandButton_Click(object sender, System.EventArgs e)
        {
            createFile(RandomFileName());
        }

        private string RandomFileName()
        {
            Random myRandom = new Random();
            return @"C:\Users\Donal\" + myRandom.Next().ToString();
        }

        private void createFile(string fileName)
        {
            try
            {
                StreamWriter sw = File.CreateText(fileName);
                sw.Close();
                statusLabel.Text = "Successfully created file: " + fileName;
                File.Delete(fileName);
            }
            catch(Exception ex)
            {
                statusLabel.Text = "Failed when attempting to create file. ";
                statusLabel.Text += ex.GetType().ToString() + ": " + ex.Message;
            }
        }

        private void declarativeDemandButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                declarativeCreateFile();
            }
            catch(Exception ex)
            {
                statusLabel.Text = "Failed before attempting to create file. ";
                statusLabel.Text += ex.GetType().ToString() + ": " + ex.Message;
            }
        }

        // Note that you cannot use a random filename in declarative permissions
        // because the filename must be statically defined.
        [FileIOPermission(SecurityAction.Demand, Write = @"C:\Users\Donal\12345")]
        private void declarativeCreateFile()
        {
            createFile(@"C:\Users\Donal\12345");
        }

        private void imperativeDemandButton_Click(object sender, System.EventArgs e)
        {
            string fileName = RandomFileName();
            FileIOPermission writeFilePermission = new FileIOPermission(FileIOPermissionAccess.Write, fileName);

            try
            {
                writeFilePermission.Demand();
                createFile(fileName);
            }
            catch(Exception ex)
            {
                statusLabel.Text = "Failed before attempting to create file. ";
                statusLabel.Text += ex.GetType().ToString() + ": " + ex.Message;
            }
        }
	}
}
