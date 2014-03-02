using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TryingAgain.Models
{
    public class TimeTrackerRepository
    {
        private TimeTrackerDbContext _context = new TimeTrackerDbContext();

        public List<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public List<TimeCard> GetTimeCardsByEmployeeID(int id)
        {
            var employee = _context.Employees.Include(y => y.TimeCards).Where(x => x.ID == id).SingleOrDefault();

            if (employee != null)
            {
                var timeCards = employee.TimeCards;
                return timeCards;
            }

            return new List<TimeCard>();
        }
    }
}