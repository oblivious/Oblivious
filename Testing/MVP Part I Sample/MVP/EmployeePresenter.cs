using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Common;

namespace MVP
{
    //-- Presenter contains the behavior
    public class EmployeePresenter
    {
        private IEmployeeView _view;
        private IEmployeeService _service;
        private EmployeeModel _model;


        public EmployeePresenter(IEmployeeView view, 
            EmployeeModel model,
            IEmployeeService service)
        {
            _view = view;
            _service = service;
            _model = model;
            _model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Model_PropertyChanged);
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            //-- if you want to process data when item value was changed.
            switch(e.PropertyName)
            {
                case "EmployeeType":
                    //-- code goes here
                    break;
                case "Salary":
                    //-- code goes here
                    break;
            }
        }

        public void Initialize()
        {
            _view.EmployeeTypes = _service.GetEmployeeTypes();
            _view.SalaryRanges = _service.GetSalaryRange(_view.EmployeeType);
        }

        private void ResetSalaryRanges()
        {
            _view.SalaryRanges = _service.GetSalaryRange(_view.EmployeeType);
        }

        private void ComputeTAXAmount()
        {
            _view.TAXAmount = _view.Salary * _view.TAX;
        }

        public ErrorMessageCollection ErrorMessages
        {
            get
            {
                return _model.ErrorMessages;
            }
        }
        /// <summary>
        /// Save data.
        /// </summary>
        public bool Save()
        {
            if (!_view.IsDirty)
                return false; //-- no changes

            _model.EmployeeID = _view.EmployeeID;
            _model.Lastname = _view.Lastname;
            _model.Firstname = _view.Firstname;
            _model.EmployeeType = _view.EmployeeType;
            _model.Salary = _view.Salary;
            _model.TAX = _view.TAX;

            //-- for tracing and debugging
            Debug.WriteLine(
                String.Format("Lastname: {0}, Firstname: {1}, EmployeeType: {2}, Salary: {3}",
                _model.Lastname,
                _model.Firstname,
                _model.EmployeeType,
                _model.TAX ));

            //-- validate the data.
            if (!_model.Validate())
            {
                Debug.WriteLine("Messages: " + ErrorMessages);
                _view.ShowValidationErrors(ErrorMessages);
                ErrorMessages.Clear();
                return false;
            }

            _service.Save(_model);

            return _model.ErrorMessages.Count == 0;
        }

        /// <summary>
        /// Confirm the user before closing the form.
        /// </summary>
        /// <param name="formClosing">set to true if you are exectuting the method inside the Form_Closing event to avoid stack overflow.</param>
        public bool CloseForm(bool formClosing)
        {
            bool result = true;

            if (_view.IsDirty)
            {
                result = _view.ConfirmClose();

                if (!result)
                {
                    return result;
                }
            }

            if (!formClosing)
            {
                _view.Close();
            }

            return result;
        }

        public void OnEmployeeTypeChanged()
        {
            ResetSalaryRanges();
        }

        public void OnEmployeeSalaryChanged()
        {
            _view.TAX = _service.GetTax(_view.Salary);
            ComputeTAXAmount();
        }

        public bool Delete(int id)
        {
            if (_view.ConfirmDelete())
            {
                _service.Delete(id);
                return true;
            }

            return false;
        }
    }
}
