using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eManager.Domain
{
    public class Department
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
