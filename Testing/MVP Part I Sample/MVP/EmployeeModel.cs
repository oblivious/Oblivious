using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Common;

namespace MVP
{
    public class EmployeeModel : IEmployee, INotifyPropertyChanged, IValidatable
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private EnumEmployeeType _employeeType;
        private double _salary;
        private float _tax;

        private ErrorMessageCollection _errorMessages;

        public EmployeeModel()
        {
            _errorMessages = new ErrorMessageCollection();
        }

        public int EmployeeID
        {
            get { return _id; }
            set 
            { 
                _id = value;

                OnPropertyChanged("EmployeeID");
            }
        }

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public EnumEmployeeType EmployeeType
        {
            get { return _employeeType; }
            set
            {
                _employeeType = value;
                OnPropertyChanged("EmployeeType");
            }
        }

        public double Salary
        {
            get { return _salary; }
            set
            {
                _salary = value;
                OnPropertyChanged("Salary");
            }
        }

        public float TAX
        {
            get { return _tax; }
            set
            {
                 _tax = value;
                 OnPropertyChanged("TAX");
            }
        }

        public ErrorMessageCollection ErrorMessages
        {
            get { return _errorMessages; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        #region IValidatable Members

        public bool Validate()
        {
            if(String.IsNullOrEmpty(Firstname))
            {
                _errorMessages.Add(new ErrorMessage("Firstname is required"));
            }

            if(String.IsNullOrEmpty(Lastname))
            {
                 _errorMessages.Add(new ErrorMessage("Lastname is required"));
            }

            if(Salary <= 0.0)
            {
                 _errorMessages.Add(new ErrorMessage("Salary must be positive number."));
            }

            if(TAX <= 0.0)
            {
                _errorMessages.Add(new ErrorMessage("TAX must be positive number."));
            }

            return _errorMessages.Count == 0; //-- no error
        }

        #endregion
    }
}
