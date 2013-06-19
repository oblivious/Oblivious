using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common;

namespace MVP
{
    public interface IEmployeeView
    {
        int EmployeeID { get;}
        string Firstname { get;}
        string Lastname { get;}
        EnumEmployeeType EmployeeType { get;}
        double TAXAmount { set;}
        double Salary { get;}
        float TAX { get; set;}

        bool IsDirty { get;}
        IList<string> SalaryRanges { set;}
        IList<string> EmployeeTypes { set;}


        bool ConfirmClose();

        void Close();

        bool ConfirmDelete();

        void ShowValidationErrors(ErrorMessageCollection errorMessages);
    }
}
