namespace WalkthroughCreatingACryptographicApplication
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
            this.buttonEncryptFile = new System.Windows.Forms.Button();
            this.buttonDecryptFile = new System.Windows.Forms.Button();
            this.buttonCreateAsmKeys = new System.Windows.Forms.Button();
            this.buttonGetPrivateKey = new System.Windows.Forms.Button();
            this.buttonExportPublicKey = new System.Windows.Forms.Button();
            this.buttonImportPublicKey = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonEncryptFile
            // 
            this.buttonEncryptFile.Location = new System.Drawing.Point(13, 13);
            this.buttonEncryptFile.Name = "buttonEncryptFile";
            this.buttonEncryptFile.Size = new System.Drawing.Size(100, 24);
            this.buttonEncryptFile.TabIndex = 0;
            this.buttonEncryptFile.Text = "Encrypt File";
            this.buttonEncryptFile.UseVisualStyleBackColor = true;
            this.buttonEncryptFile.Click += new System.EventHandler(this.buttonEncryptFile_Click);
            // 
            // buttonDecryptFile
            // 
            this.buttonDecryptFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDecryptFile.Location = new System.Drawing.Point(127, 13);
            this.buttonDecryptFile.Name = "buttonDecryptFile";
            this.buttonDecryptFile.Size = new System.Drawing.Size(100, 24);
            this.buttonDecryptFile.TabIndex = 1;
            this.buttonDecryptFile.Text = "Decrypt File";
            this.buttonDecryptFile.UseVisualStyleBackColor = true;
            this.buttonDecryptFile.Click += new System.EventHandler(this.buttonDecryptFile_Click);
            // 
            // buttonCreateAsmKeys
            // 
            this.buttonCreateAsmKeys.Location = new System.Drawing.Point(12, 42);
            this.buttonCreateAsmKeys.Name = "buttonCreateAsmKeys";
            this.buttonCreateAsmKeys.Size = new System.Drawing.Size(100, 24);
            this.buttonCreateAsmKeys.TabIndex = 2;
            this.buttonCreateAsmKeys.Text = "Create Keys";
            this.buttonCreateAsmKeys.UseVisualStyleBackColor = true;
            this.buttonCreateAsmKeys.Click += new System.EventHandler(this.buttonCreateAsmKeys_Click);
            // 
            // buttonGetPrivateKey
            // 
            this.buttonGetPrivateKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetPrivateKey.Location = new System.Drawing.Point(127, 42);
            this.buttonGetPrivateKey.Name = "buttonGetPrivateKey";
            this.buttonGetPrivateKey.Size = new System.Drawing.Size(100, 24);
            this.buttonGetPrivateKey.TabIndex = 3;
            this.buttonGetPrivateKey.Text = "Get Private Key";
            this.buttonGetPrivateKey.UseVisualStyleBackColor = true;
            this.buttonGetPrivateKey.Click += new System.EventHandler(this.buttonGetPrivateKey_Click);
            // 
            // buttonExportPublicKey
            // 
            this.buttonExportPublicKey.Location = new System.Drawing.Point(12, 71);
            this.buttonExportPublicKey.Name = "buttonExportPublicKey";
            this.buttonExportPublicKey.Size = new System.Drawing.Size(100, 24);
            this.buttonExportPublicKey.TabIndex = 4;
            this.buttonExportPublicKey.Text = "Export Public Key";
            this.buttonExportPublicKey.UseVisualStyleBackColor = true;
            this.buttonExportPublicKey.Click += new System.EventHandler(this.buttonExportPublicKey_Click);
            // 
            // buttonImportPublicKey
            // 
            this.buttonImportPublicKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImportPublicKey.Location = new System.Drawing.Point(127, 71);
            this.buttonImportPublicKey.Name = "buttonImportPublicKey";
            this.buttonImportPublicKey.Size = new System.Drawing.Size(100, 24);
            this.buttonImportPublicKey.TabIndex = 5;
            this.buttonImportPublicKey.Text = "Import Public Key";
            this.buttonImportPublicKey.UseVisualStyleBackColor = true;
            this.buttonImportPublicKey.Click += new System.EventHandler(this.buttonImportPublicKey_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 122);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonImportPublicKey);
            this.Controls.Add(this.buttonExportPublicKey);
            this.Controls.Add(this.buttonGetPrivateKey);
            this.Controls.Add(this.buttonCreateAsmKeys);
            this.Controls.Add(this.buttonDecryptFile);
            this.Controls.Add(this.buttonEncryptFile);
            this.Name = "Form1";
            this.Text = "Cryptosity";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEncryptFile;
        private System.Windows.Forms.Button buttonDecryptFile;
        private System.Windows.Forms.Button buttonCreateAsmKeys;
        private System.Windows.Forms.Button buttonGetPrivateKey;
        private System.Windows.Forms.Button buttonExportPublicKey;
        private System.Windows.Forms.Button buttonImportPublicKey;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label1;
    }
}

