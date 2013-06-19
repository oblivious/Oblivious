using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace MVP
{
    public class EmployeeService : IEmployeeService
    {

        #region IEmployeeService Members

        public bool IsExists(int id)
        {
            //-- [call to BLL or DAL goes here].

            //-- this is dummy data and logic
            if (id == 0)
                return false;

            return false;
        }

        public void Save(IEmployee employee)
        {
            if(IsExists(employee.EmployeeID))
            {
                Update(employee);
            }

            else
            {
                Insert(employee);
            }
        }

        private static void Update(IEmployee employee)
        {
            //-- [call to BLL or DAL goes here].
        }

        private static void Insert(IEmployee employee)
        {
            //-- [call to BLL or DAL goes here].
        }

        public void Delete(int id)
        {
            //-- [call to BLL or DAL goes here].
        }

        public IList<string> GetSalaryRange(EnumEmployeeType employeeType)
        {
            //-- [call to BLL or DAL goes here].
            
            //-- this is dummy data
            List<string> list = new List<string>();

            switch(employeeType)
            {
                case EnumEmployeeType.Admin:
                    list.Add("1000");
                    list.Add("2000");
                    list.Add("3000");
                    break;

                case EnumEmployeeType.HumanResources:
                    list.Add("4000");
                    list.Add("5000");
                    list.Add("6000");
                    break;

                case EnumEmployeeType.Manager:
                    list.Add("7000");
                    list.Add("8000");
                    list.Add("9000");
                    break;

                case EnumEmployeeType.MIS:
                    list.Add("10000");
                    list.Add("11000");
                    list.Add("12000");
                    break;

                case EnumEmployeeType.Sales:
                    list.Add("13000");
                    list.Add("14000");
                    list.Add("15000");
                    break;

                case EnumEmployeeType.Technical:
                    list.Add("16000");
                    list.Add("17000");
                    list.Add("18000");
                    break;
            }

            return list;
        }

        public IList<string> GetEmployeeTypes()
        {
            //-- [call to BLL or DAL goes here].

            //-- this is dummy data.
            List<string> list = new List<string>();

            list.Add(EnumEmployeeType.Admin.ToString());
            list.Add(EnumEmployeeType.HumanResources.ToString());
            list.Add(EnumEmployeeType.Manager.ToString());
            list.Add(EnumEmployeeType.MIS.ToString());
            list.Add(EnumEmployeeType.Sales.ToString());
            list.Add(EnumEmployeeType.Technical.ToString());

            return list;
        }

        public float GetTax(double salary)
        {
            //-- [call to BLL or DAL goes here].

            //-- dummy tax logic and data
            if(salary > 0 && salary < 4000)
            {
                return 0.10f;
            }

            else if (salary > 4000 && salary < 8000)
            {
                return 0.15f;
            }

            else if (salary > 8000 && salary < 12000)
            {
                return 0.20f;
            }

            else if (salary > 12000 && salary < 16000)
            {
                return 0.25f;
            }

            return 0.10f;
        }

        #endregion
    }
}
