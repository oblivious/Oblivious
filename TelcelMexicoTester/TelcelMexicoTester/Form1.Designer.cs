namespace TelcelMexicoTester
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.sendLocalServiceStartupButton = new System.Windows.Forms.Button();
			this.sendCommsTestButton = new System.Windows.Forms.Button();
			this.SocketSendReceiveButton = new System.Windows.Forms.Button();
			this.tConsole = new System.Windows.Forms.RichTextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// sendLocalServiceStartupButton
			// 
			this.sendLocalServiceStartupButton.Location = new System.Drawing.Point(13, 13);
			this.sendLocalServiceStartupButton.Name = "sendLocalServiceStartupButton";
			this.sendLocalServiceStartupButton.Size = new System.Drawing.Size(137, 23);
			this.sendLocalServiceStartupButton.TabIndex = 0;
			this.sendLocalServiceStartupButton.Text = "SendLocalServiceStartup";
			this.sendLocalServiceStartupButton.UseVisualStyleBackColor = true;
			this.sendLocalServiceStartupButton.Click += new System.EventHandler(this.sendLocalServiceStartupButton_Click);
			// 
			// sendCommsTestButton
			// 
			this.sendCommsTestButton.Location = new System.Drawing.Point(13, 43);
			this.sendCommsTestButton.Name = "sendCommsTestButton";
			this.sendCommsTestButton.Size = new System.Drawing.Size(137, 23);
			this.sendCommsTestButton.TabIndex = 1;
			this.sendCommsTestButton.Text = "SendCommsTest";
			this.sendCommsTestButton.UseVisualStyleBackColor = true;
			this.sendCommsTestButton.Click += new System.EventHandler(this.sendCommsTestButton_Click);
			// 
			// SocketSendReceiveButton
			// 
			this.SocketSendReceiveButton.Location = new System.Drawing.Point(13, 73);
			this.SocketSendReceiveButton.Name = "SocketSendReceiveButton";
			this.SocketSendReceiveButton.Size = new System.Drawing.Size(137, 23);
			this.SocketSendReceiveButton.TabIndex = 2;
			this.SocketSendReceiveButton.Text = "SocketSendReceive";
			this.SocketSendReceiveButton.UseVisualStyleBackColor = true;
			this.SocketSendReceiveButton.Click += new System.EventHandler(this.SocketSendReceiveButton_Click);
			// 
			// tConsole
			// 
			this.tConsole.Location = new System.Drawing.Point(13, 103);
			this.tConsole.Name = "tConsole";
			this.tConsole.Size = new System.Drawing.Size(532, 321);
			this.tConsole.TabIndex = 3;
			this.tConsole.Text = "";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(157, 73);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(388, 20);
			this.textBox1.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(557, 436);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.tConsole);
			this.Controls.Add(this.SocketSendReceiveButton);
			this.Controls.Add(this.sendCommsTestButton);
			this.Controls.Add(this.sendLocalServiceStartupButton);
			this.Name = "Form1";
			this.Text = "Telcel Tester";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button sendLocalServiceStartupButton;
		private System.Windows.Forms.Button sendCommsTestButton;
		private System.Windows.Forms.Button SocketSendReceiveButton;
		private System.Windows.Forms.RichTextBox tConsole;
		private System.Windows.Forms.TextBox textBox1;
	}
}

