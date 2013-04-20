using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Security.Permissions;
using System.Security.Principal;

namespace DeclarativeRBS
{
	/// <summary>
	/// GUI app that provides access to highly confidential algorithms.
	/// </summary>

    public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button multiplyButton;
        private System.Windows.Forms.Label answerLabel;
        private System.Windows.Forms.TextBox integer1;
        private System.Windows.Forms.TextBox integer2;
        private System.Windows.Forms.Button subtractButton;
        private System.Windows.Forms.Button divideButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();

            WindowsPrincipal currentPrincipal = new WindowsPrincipal(currentIdentity);

            System.AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            if (!currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                subtractButton.Visible = false;
                multiplyButton.Visible = false;
            }
            
            if (!currentPrincipal.IsInRole(WindowsBuiltInRole.User))
                addButton.Visible = false;

            if (!(currentIdentity.Name.ToLower() == System.Environment.MachineName.ToLower() + @"\donal"))
                divideButton.Visible = false;
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
            this.addButton = new System.Windows.Forms.Button();
            this.integer1 = new System.Windows.Forms.TextBox();
            this.integer2 = new System.Windows.Forms.TextBox();
            this.multiplyButton = new System.Windows.Forms.Button();
            this.answerLabel = new System.Windows.Forms.Label();
            this.subtractButton = new System.Windows.Forms.Button();
            this.divideButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(32, 40);
            this.addButton.Name = "addButton";
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // integer1
            // 
            this.integer1.Location = new System.Drawing.Point(16, 8);
            this.integer1.Name = "integer1";
            this.integer1.Size = new System.Drawing.Size(112, 20);
            this.integer1.TabIndex = 1;
            this.integer1.Text = "2";
            this.integer1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // integer2
            // 
            this.integer2.Location = new System.Drawing.Point(144, 8);
            this.integer2.Name = "integer2";
            this.integer2.Size = new System.Drawing.Size(128, 20);
            this.integer2.TabIndex = 1;
            this.integer2.Text = "3";
            this.integer2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // multiplyButton
            // 
            this.multiplyButton.Location = new System.Drawing.Point(168, 40);
            this.multiplyButton.Name = "multiplyButton";
            this.multiplyButton.TabIndex = 0;
            this.multiplyButton.Text = "Multiply";
            this.multiplyButton.Click += new System.EventHandler(this.multiplyButton_Click);
            // 
            // answerLabel
            // 
            this.answerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.answerLabel.Location = new System.Drawing.Point(24, 112);
            this.answerLabel.Name = "answerLabel";
            this.answerLabel.Size = new System.Drawing.Size(248, 24);
            this.answerLabel.TabIndex = 2;
            this.answerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subtractButton
            // 
            this.subtractButton.Location = new System.Drawing.Point(32, 72);
            this.subtractButton.Name = "subtractButton";
            this.subtractButton.TabIndex = 0;
            this.subtractButton.Text = "Subtract";
            this.subtractButton.Click += new System.EventHandler(this.subtractButton_Click);
            // 
            // divideButton
            // 
            this.divideButton.Location = new System.Drawing.Point(168, 72);
            this.divideButton.Name = "divideButton";
            this.divideButton.TabIndex = 0;
            this.divideButton.Text = "Divide";
            this.divideButton.Click += new System.EventHandler(this.divideButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 141);
            this.Controls.Add(this.answerLabel);
            this.Controls.Add(this.integer1);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.integer2);
            this.Controls.Add(this.multiplyButton);
            this.Controls.Add(this.subtractButton);
            this.Controls.Add(this.divideButton);
            this.Name = "Form1";
            this.Text = "Super-secret math form";
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main() 
		{
            Application.Run(new Form1());
		}

        private void addButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                PrincipalPermission userPermission = new PrincipalPermission(null, @"BUILTIN\Users");
                userPermission.Demand();

                int answer = (int.Parse(integer1.Text) + int.Parse(integer2.Text));
                answerLabel.Text = answer.ToString();
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("You have been denied access: " + ex.Message);
                //TODO: Log the error...
            }
        }

        private void multiplyButton_Click(object sender, System.EventArgs e)
        {
            answerLabel.Text = multiply(int.Parse(integer1.Text), int.Parse(integer2.Text)).ToString();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        private int multiply(int int1, int int2)
        {
            return int1 * int2;
        }

        private void subtractButton_Click(object sender, System.EventArgs e)
        {
            int answer = (int.Parse(integer1.Text) - int.Parse(integer2.Text));
            answerLabel.Text = answer.ToString();
        }

        private void divideButton_Click(object sender, System.EventArgs e)
        {
            string allowUser = System.Environment.MachineName + @"\cphilips";
            try
            {
                PrincipalPermission p = new PrincipalPermission(allowUser, null);
                p.Demand();

                Decimal answer = (Decimal.Parse(integer1.Text) / Decimal.Parse(integer2.Text));
                answerLabel.Text = Decimal.Round(answer, 2).ToString();
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("You have been denied access: " + ex.Message);
            }
        }
	}
}
