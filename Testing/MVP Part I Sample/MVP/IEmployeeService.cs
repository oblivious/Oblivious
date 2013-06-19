using System;
using System.Collections.Generic;
using System.Text;

namespace MVP
{
    public interface IEmployeeService
    {
        bool IsExists(int id);

        void Save(IEmployee employee);
        
        void Delete(int id);

        IList<string> GetSalaryRange(Common.EnumEmployeeType enumEmployeeType);

        IList<string> GetEmployeeTypes();

        float GetTax(double salary);
    }
}
