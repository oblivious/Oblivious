namespace Drawing_Tests
{
    partial class DrawingTest
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
            this.btnGraphicsClass = new System.Windows.Forms.Button();
            this.btnMoreGraphicsClass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGraphicsClass
            // 
            this.btnGraphicsClass.Location = new System.Drawing.Point(12, 12);
            this.btnGraphicsClass.Name = "btnGraphicsClass";
            this.btnGraphicsClass.Size = new System.Drawing.Size(89, 23);
            this.btnGraphicsClass.TabIndex = 0;
            this.btnGraphicsClass.Text = "Graphics!";
            this.btnGraphicsClass.UseVisualStyleBackColor = true;
            this.btnGraphicsClass.Click += new System.EventHandler(this.btnGraphicsClass_Click);
            // 
            // btnMoreGraphicsClass
            // 
            this.btnMoreGraphicsClass.Location = new System.Drawing.Point(13, 42);
            this.btnMoreGraphicsClass.Name = "btnMoreGraphicsClass";
            this.btnMoreGraphicsClass.Size = new System.Drawing.Size(88, 23);
            this.btnMoreGraphicsClass.TabIndex = 1;
            this.btnMoreGraphicsClass.Text = "More Graphics!";
            this.btnMoreGraphicsClass.UseVisualStyleBackColor = true;
            this.btnMoreGraphicsClass.Click += new System.EventHandler(this.btnMoreGraphicsClass_Click);
            // 
            // DrawingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 471);
            this.Controls.Add(this.btnMoreGraphicsClass);
            this.Controls.Add(this.btnGraphicsClass);
            this.Name = "DrawingTest";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGraphicsClass;
        private System.Windows.Forms.Button btnMoreGraphicsClass;
    }
}