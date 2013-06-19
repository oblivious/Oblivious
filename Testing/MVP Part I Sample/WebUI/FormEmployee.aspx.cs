using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using Common;
using MVP;

namespace WebUI
{
    public partial class FormEmployee : System.Web.UI.Page, IEmployeeView
    {
        private int _id = 0;
        private EmployeePresenter _presenter;
        private bool _suppressEvents = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new EmployeePresenter(this,
                                               new EmployeeModel(),
                                               new EmployeeService());

            if (!IsPostBack)
            {
                LabelErrorMessage.Text = String.Empty;

                try
                {
                    _suppressEvents = true;
                    //-- initialize presenter
                    _presenter.Initialize();
                    //_isDirty = false; //-- web is stateless
                }

                finally
                {
                    _suppressEvents = false;
                }
            }
        }

        //NOTE: Input_Changed event is not added in web. Its not a good to track every key stroke in web.

        protected void DropDownListEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            //_isDirty = true;
            _presenter.OnEmployeeTypeChanged();
        }

        protected void DropDownListSalary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            //_isDirty = true;
            _presenter.OnEmployeeSalaryChanged();
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
                return TextBoxFirstname.Text;
            }
        }

        public string Lastname
        {
            get
            {
                return TextBoxLastname.Text;
            }
        }

        public EnumEmployeeType EmployeeType
        {
            get
            {
                if (DropDownListEmployeeType.SelectedIndex != -1)
                {
                    return Enums.GetEmployeeTypeByName(
                        DropDownListEmployeeType.SelectedItem.Text);
                }

                return EnumEmployeeType.Admin;
            }
        }

        public double TAXAmount
        {
            set { TextBoxTaxAmount.Text = value.ToString("0.00"); }
        }

        public double Salary
        {
            get { return Convert.ToDouble(DropDownListSalary.SelectedItem.Text); }
        }

        public float TAX
        {
            get { return Convert.ToSingle(TextBoxTAX.Text); }
            set { TextBoxTAX.Text = value.ToString("0.00"); }
        }

        public bool IsDirty
        {
            //-- web is statless just make this false. Set dirty to true inorder to process Save.
            get { return true; }
        }

        public IList<string> SalaryRanges
        {
            set
            {
                DropDownListSalary.DataSource = value;
                DropDownListSalary.DataBind();
                DropDownListSalary.SelectedIndex = 0;
            }
        }

        public IList<string> EmployeeTypes
        {
            set
            {
                DropDownListEmployeeType.DataSource = value;
                DropDownListEmployeeType.DataBind();
                DropDownListEmployeeType.SelectedIndex = 0;
            }
        }

        public bool ConfirmClose()
        {
            //-- web is stateless. this should be handled by separate page and/or a javascript popup. 
            //-- In web application it rarely happens that the user will be confirm the user before closing the window.
            throw new NotSupportedException("Not supported in web.");
        }

        public bool ConfirmDelete()
        {
            //-- web is stateless. this should be handled by separate page and/or a javascript popup
            throw new NotSupportedException("Not supported in web.");
        }

        public void Close()
        {
            //-- web is stateless. this should be handled by separate page and/or a javascript popup
            throw new NotSupportedException("Not supported in web.");
        }

        public void ShowValidationErrors(ErrorMessageCollection errorMessages)
        {
            LabelErrorMessage.Text = errorMessages.ToString();
        }

        #endregion

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (_presenter.Save())
            {
                Response.Redirect("Default.aspx");    
                return;
            }

            
        }
    }
}
