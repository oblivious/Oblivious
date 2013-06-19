using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common;

namespace MVP
{
    public interface IEmployee
    {
        int EmployeeID { get; set;}
        string Firstname { get; set;}
        string Lastname { get; set;}
        EnumEmployeeType EmployeeType { get; set;}
        double Salary { get; set;}
        float TAX { get; set;}
    }
}
