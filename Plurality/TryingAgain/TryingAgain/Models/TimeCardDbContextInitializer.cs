using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TryingAgain.Models
{
    public class TimeCardDbContextInitializer : DropCreateDatabaseIfModelChanges<TimeTrackerDbContext>
    {
        protected override void Seed(TimeTrackerDbContext context)
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    ID = 1, FirstName = "John", LastName = "Doe", Department = "Finance", HireDate = new DateTime(1988, 5, 14),
                    TimeCards = new List<TimeCard> 
                    {
                        new TimeCard { ID = 1, SubmissionDate = DateTime.Now, MondayHours = 8, TuesdayHours = 7, WednesdayHours = 9, ThursdayHours = 8, FridayHours = 7, SaturdayHours = 0, SundayHours = 0},
                        new TimeCard { ID = 2, SubmissionDate = DateTime.Now.AddDays(-7), MondayHours = 9, TuesdayHours = 8, WednesdayHours = 7, ThursdayHours = 9, FridayHours = 8, SaturdayHours = 0, SundayHours = 0}
                    }
                },
                new Employee
                {
                    ID = 1, FirstName = "Jane", LastName = "Doe", Department = "HR", HireDate = new DateTime(1991, 3, 22),
                    TimeCards = new List<TimeCard> 
                    {
                        new TimeCard { ID = 1, SubmissionDate = DateTime.Now, MondayHours = 8, TuesdayHours = 7, WednesdayHours = 9, ThursdayHours = 8, FridayHours = 7, SaturdayHours = 0, SundayHours = 0},
                        new TimeCard { ID = 2, SubmissionDate = DateTime.Now.AddDays(-7), MondayHours = 9, TuesdayHours = 8, WednesdayHours = 7, ThursdayHours = 9, FridayHours = 8, SaturdayHours = 0, SundayHours = 0}
                    }
                }
            };

            employees.ForEach(e => context.Employees.Add(e));
            base.Seed(context);
        }
    }
}