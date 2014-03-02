using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC3.Models
{
    public static class TaskManager
    {
        public static List<Task> GetTasks()
        {
            var db = new TestDBEntities();
            return db.Tasks.ToList();
        }
    }
}