namespace CASArticleQuickExample
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
            this.readButton = new System.Windows.Forms.Button();
            this.writeButton = new System.Windows.Forms.Button();
            this.btnFileRead = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.someButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(93, 97);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(75, 23);
            this.readButton.TabIndex = 0;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(174, 97);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(75, 23);
            this.writeButton.TabIndex = 1;
            this.writeButton.Text = "Write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // btnFileRead
            // 
            this.btnFileRead.Location = new System.Drawing.Point(12, 97);
            this.btnFileRead.Name = "btnFileRead";
            this.btnFileRead.Size = new System.Drawing.Size(75, 23);
            this.btnFileRead.TabIndex = 2;
            this.btnFileRead.Text = "File Read";
            this.btnFileRead.UseVisualStyleBackColor = true;
            this.btnFileRead.Click += new System.EventHandler(this.btnFileRead_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(12, 68);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(93, 23);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Link Inheritance";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // someButton
            // 
            this.someButton.Location = new System.Drawing.Point(13, 13);
            this.someButton.Name = "someButton";
            this.someButton.Size = new System.Drawing.Size(234, 32);
            this.someButton.TabIndex = 4;
            this.someButton.Text = "The Power of Activation Context!";
            this.someButton.UseVisualStyleBackColor = true;
            this.someButton.Click += new System.EventHandler(this.someButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 132);
            this.Controls.Add(this.someButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.btnFileRead);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.readButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.Button btnFileRead;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button someButton;
    }
}

