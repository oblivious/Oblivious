using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using MVP;

namespace WinUI
{
    public partial class FormEmployee : Form, IEmployeeView
    {
        private EmployeePresenter _presenter;
        private bool _isDirty;
        private int _id = 0;

        private bool _suppressEvents = false;

        public FormEmployee()
        {
            InitializeComponent();

        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            _presenter = new EmployeePresenter(this,
                                               new EmployeeModel(),
                                               new EmployeeService());//-- NOTE: using employee web service

            try
            {
                _suppressEvents = true;
                //-- initialize presenter
                _presenter.Initialize();
                _isDirty = false;
            }

            finally
            {
                _suppressEvents = false;
            }
        }

        #region IEmployeeView Members

        public int EmployeeID
        {
            get
            {
                return _id;
            }
        }

        public string Firstname
        {
            get 
            {
                return textBoxFirstname.Text;
            }
        }

        public string Lastname
        {
            get 
            {
                return textBoxLastname.Text;
            }
        }

        public EnumEmployeeType EmployeeType
        {
            get 
            {
                return Enums.GetEmployeeTypeByName(
                    comboBoxEmployeeType.SelectedItem.ToString());
            }
        }

        public double TAXAmount
        {
            set { textBoxTaxAmount.Text = value.ToString("0.00"); }
        }

        public double Salary
        {
            get { return Convert.ToDouble(comboBoxSalary.SelectedItem.ToString()); }
        }

        public float TAX
        {
            get { return Convert.ToSingle(textBoxTAX.Text); }
            set { textBoxTAX.Text = value.ToString("0.00"); }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
        }

        public IList<string> SalaryRanges
        {
            set
            {
                comboBoxSalary.DataSource = value;
            }
        }

        public IList<string> EmployeeTypes
        {
            set 
            {
                comboBoxEmployeeType.DataSource = value;
            }
        }

        public bool ConfirmClose()
        {
            return MessageBox.Show("Are you sure you want to proceed?", "Question",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public bool ConfirmDelete()
        {
            return MessageBox.Show("Are you sure you want to delete?", "Question",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public void ShowValidationErrors(ErrorMessageCollection errorMessages)
        {
            MessageBox.Show(errorMessages.ToString());
        }

        #endregion

        private void FormEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_presenter.CloseForm(true);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_presenter.Save())
            {
                _isDirty = false;
                MessageBox.Show("Successfully saved.");
                Close();
            }
        }

        private void Input_Changed(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            _isDirty = true;
        }

        private void comboBoxEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            _isDirty = true;
            _presenter.OnEmployeeTypeChanged();
        }

        private void comboBoxSalary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            _isDirty = true;
            _presenter.OnEmployeeSalaryChanged();
        }
    }
}