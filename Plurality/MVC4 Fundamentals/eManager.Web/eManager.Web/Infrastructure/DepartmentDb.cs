using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using eManager.Domain;

namespace eManager.Web.InfraStructure
{
    public class DepartmentDb : DbContext, IDepartmentDataSource
    {
        public DepartmentDb() : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        IQueryable<Employee> IDepartmentDataSource.Employees
        {
            get { return Employees; }
        }

        IQueryable<Department> IDepartmentDataSource.Departments
        {
            get { return Departments; }
        }
    }
}