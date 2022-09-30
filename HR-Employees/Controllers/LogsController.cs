using HR_Employees.Models.BusinessModels;
using HR_Employees.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Employees.Controllers
{
    public class LogsController : Controller
    {
        private readonly HRContext _context;

        public LogsController(HRContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var hRContext =
                _context.Logs.
                Where(l => l.EmployeeID == id)
                .GroupBy(l => l.DateTime.Date)
                .Where(g => g.Any(l => l.IsIn) && g.Any(l => !l.IsIn))
                .Select(g => new DayLog()
                {
                    EmployeeName = g.First().Employee.Name,
                    SignIn = g.Where(l => l.IsIn).OrderBy(l => l.DateTime).First().DateTime,
                    SignOut = g.Where(l => !l.IsIn).OrderBy(l => l.DateTime).Last().DateTime,
                });

            return View(await hRContext.ToListAsync());
        }
    }
}