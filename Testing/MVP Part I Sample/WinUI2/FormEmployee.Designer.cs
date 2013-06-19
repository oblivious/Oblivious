namespace WinUI2
{
    partial class FormEmployee
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
            this.textBoxTaxAmount = new System.Windows.Forms.TextBox();
            this.labelTaxAmount = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxTAX = new System.Windows.Forms.TextBox();
            this.labelTax = new System.Windows.Forms.Label();
            this.labelSalary = new System.Windows.Forms.Label();
            this.labelEmployeeType = new System.Windows.Forms.Label();
            this.comboBoxEmployeeType = new System.Windows.Forms.ComboBox();
            this.textBoxLastname = new System.Windows.Forms.TextBox();
            this.labelLastname = new System.Windows.Forms.Label();
            this.textBoxFirstname = new System.Windows.Forms.TextBox();
            this.labelFirsname = new System.Windows.Forms.Label();
            this.listBoxSalary = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBoxTaxAmount
            // 
            this.textBoxTaxAmount.Location = new System.Drawing.Point(101, 191);
            this.textBoxTaxAmount.Name = "textBoxTaxAmount";
            this.textBoxTaxAmount.ReadOnly = true;
            this.textBoxTaxAmount.Size = new System.Drawing.Size(179, 20);
            this.textBoxTaxAmount.TabIndex = 27;
            // 
            // labelTaxAmount
            // 
            this.labelTaxAmount.AutoSize = true;
            this.labelTaxAmount.Location = new System.Drawing.Point(12, 194);
            this.labelTaxAmount.Name = "labelTaxAmount";
            this.labelTaxAmount.Size = new System.Drawing.Size(70, 13);
            this.labelTaxAmount.TabIndex = 26;
            this.labelTaxAmount.Text = "TAX Amount:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(124, 233);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 25;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(205, 233);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 24;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxTAX
            // 
            this.textBoxTAX.Location = new System.Drawing.Point(101, 165);
            this.textBoxTAX.Name = "textBoxTAX";
            this.textBoxTAX.ReadOnly = true;
            this.textBoxTAX.Size = new System.Drawing.Size(179, 20);
            this.textBoxTAX.TabIndex = 23;
            this.textBoxTAX.Text = "0.10";
            // 
            // labelTax
            // 
            this.labelTax.AutoSize = true;
            this.labelTax.Location = new System.Drawing.Point(12, 168);
            this.labelTax.Name = "labelTax";
            this.labelTax.Size = new System.Drawing.Size(31, 13);
            this.labelTax.TabIndex = 22;
            this.labelTax.Text = "TAX:";
            // 
            // labelSalary
            // 
            this.labelSalary.AutoSize = true;
            this.labelSalary.Location = new System.Drawing.Point(12, 120);
            this.labelSalary.Name = "labelSalary";
            this.labelSalary.Size = new System.Drawing.Size(39, 13);
            this.labelSalary.TabIndex = 20;
            this.labelSalary.Text = "Salary:";
            // 
            // labelEmployeeType
            // 
            this.labelEmployeeType.AutoSize = true;
            this.labelEmployeeType.Location = new System.Drawing.Point(12, 93);
            this.labelEmployeeType.Name = "labelEmployeeType";
            this.labelEmployeeType.Size = new System.Drawing.Size(83, 13);
            this.labelEmployeeType.TabIndex = 18;
            this.labelEmployeeType.Text = "Employee Type:";
            // 
            // comboBoxEmployeeType
            // 
            this.comboBoxEmployeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmployeeType.FormattingEnabled = true;
            this.comboBoxEmployeeType.Location = new System.Drawing.Point(101, 90);
            this.comboBoxEmployeeType.Name = "comboBoxEmployeeType";
            this.comboBoxEmployeeType.Size = new System.Drawing.Size(179, 21);
            this.comboBoxEmployeeType.TabIndex = 19;
            this.comboBoxEmployeeType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEmployeeType_SelectedIndexChanged);
            // 
            // textBoxLastname
            // 
            this.textBoxLastname.Location = new System.Drawing.Point(101, 64);
            this.textBoxLastname.Name = "textBoxLastname";
            this.textBoxLastname.Size = new System.Drawing.Size(179, 20);
            this.textBoxLastname.TabIndex = 17;
            this.textBoxLastname.TextChanged += new System.EventHandler(this.Input_Changed);
            // 
            // labelLastname
            // 
            this.labelLastname.AutoSize = true;
            this.labelLastname.Location = new System.Drawing.Point(12, 67);
            this.labelLastname.Name = "labelLastname";
            this.labelLastname.Size = new System.Drawing.Size(56, 13);
            this.labelLastname.TabIndex = 16;
            this.labelLastname.Text = "Lastname:";
            // 
            // textBoxFirstname
            // 
            this.textBoxFirstname.Location = new System.Drawing.Point(101, 38);
            this.textBoxFirstname.Name = "textBoxFirstname";
            this.textBoxFirstname.Size = new System.Drawing.Size(179, 20);
            this.textBoxFirstname.TabIndex = 15;
            this.textBoxFirstname.TextChanged += new System.EventHandler(this.Input_Changed);
            // 
            // labelFirsname
            // 
            this.labelFirsname.AutoSize = true;
            this.labelFirsname.Location = new System.Drawing.Point(12, 41);
            this.labelFirsname.Name = "labelFirsname";
            this.labelFirsname.Size = new System.Drawing.Size(55, 13);
            this.labelFirsname.TabIndex = 14;
            this.labelFirsname.Text = "Firstname:";
            // 
            // listBoxSalary
            // 
            this.listBoxSalary.FormattingEnabled = true;
            this.listBoxSalary.Location = new System.Drawing.Point(101, 120);
            this.listBoxSalary.Name = "listBoxSalary";
            this.listBoxSalary.Size = new System.Drawing.Size(179, 43);
            this.listBoxSalary.TabIndex = 28;
            this.listBoxSalary.SelectedIndexChanged += new System.EventHandler(this.listBoxSalary_SelectedIndexChanged);
            // 
            // FormEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.listBoxSalary);
            this.Controls.Add(this.textBoxTaxAmount);
            this.Controls.Add(this.labelTaxAmount);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxTAX);
            this.Controls.Add(this.labelTax);
            this.Controls.Add(this.labelSalary);
            this.Controls.Add(this.labelEmployeeType);
            this.Controls.Add(this.comboBoxEmployeeType);
            this.Controls.Add(this.textBoxLastname);
            this.Controls.Add(this.labelLastname);
            this.Controls.Add(this.textBoxFirstname);
            this.Controls.Add(this.labelFirsname);
            this.Name = "FormEmployee";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormEmployee_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEmployee_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTaxAmount;
        private System.Windows.Forms.Label labelTaxAmount;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxTAX;
        private System.Windows.Forms.Label labelTax;
        private System.Windows.Forms.Label labelSalary;
        private System.Windows.Forms.Label labelEmployeeType;
        private System.Windows.Forms.ComboBox comboBoxEmployeeType;
        private System.Windows.Forms.TextBox textBoxLastname;
        private System.Windows.Forms.Label labelLastname;
        private System.Windows.Forms.TextBox textBoxFirstname;
        private System.Windows.Forms.Label labelFirsname;
        private System.Windows.Forms.ListBox listBoxSalary;
    }
}

