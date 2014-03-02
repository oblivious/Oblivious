using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracker.Models
{
    public class TimeTrackerRepository
    {
        TimeTrackerDbContext _Context = new TimeTrackerDbContext();

        public List<Employee> GetEmployees()
        {
            return _Context.Employees.OrderBy(x => x.LastName).ToList();//(from e in _Context.Employees select e).ToList();
        }

        public int GetEmployeeCount()
        {
            return _Context.Employees.Count();
        }

        public List<TimeCard> GetTimeCardsByDate(DateTime submissionDate)
        {
            return _Context.TimeCards.Where(x => x.SubmissionDate.Date == submissionDate.Date).ToList();
        }

        public List<TimeCard> GetEmployeeTimeCards(int id)
        {
            return _Context.Employees.Where(x => x.ID == id + 1).SingleOrDefault().TimeCards;
        }
    }
}