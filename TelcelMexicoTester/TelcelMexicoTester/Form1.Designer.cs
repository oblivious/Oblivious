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
			// 
			// SocketSendReceiveButton
			// 
			this.SocketSendReceiveButton.Location = new System.Drawing.Point(13, 73);
			this.SocketSendReceiveButton.Name = "SocketSendReceiveButton";
			this.SocketSendReceiveButton.Size = new System.Drawing.Size(137, 23);
			this.SocketSendReceiveButton.TabIndex = 2;
			this.SocketSendReceiveButton.Text = "SocketSendReceive";
			this.SocketSendReceiveButton.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 109);
			this.Controls.Add(this.SocketSendReceiveButton);
			this.Controls.Add(this.sendCommsTestButton);
			this.Controls.Add(this.sendLocalServiceStartupButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button sendLocalServiceStartupButton;
		private System.Windows.Forms.Button sendCommsTestButton;
		private System.Windows.Forms.Button SocketSendReceiveButton;
	}
}

