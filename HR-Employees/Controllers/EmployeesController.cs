using HR_Employees.Models.DBModels;
using HR_Employees.Models.DBModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_Employees.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly HRContext _context;

		public EmployeesController(HRContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(int? employeeID, string employeeName, int? managerID, string managerName)
		{
			IQueryable<Employee> employees = _context.Employees.Include(e => e.Manager);

			if (employeeID.HasValue)
				employees = employees.Where(e => e.ID == employeeID.Value);

			if (!string.IsNullOrEmpty(employeeName) && !string.IsNullOrWhiteSpace(employeeName))
				employees = employees.Where(e => e.Name == employeeName);

			if (managerID.HasValue)
				employees = employees.Where(e => e.ManagerID == managerID.Value);

			if (!string.IsNullOrEmpty(managerName) && !string.IsNullOrWhiteSpace(managerName))
				employees = employees.Where(e => e.Manager.Name == managerName);

			return View(await employees.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,ManagerID,Name,Address,BirthDate,Email,Mobile")] Employee employee)
		{
			if (_context.Employees.Any(e => e.ID == employee.ID))
				ModelState.AddModelError(nameof(Employee.ID), $"{nameof(Employee)} with {nameof(Employee.ID)} already exists");

			ValidateModel(employee);

			if (ModelState.IsValid)
			{
				_context.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(employee);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Employees == null)
			{
				return NotFound();
			}

			var employee = await _context.Employees.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,ManagerID,Name,Address,BirthDate,Email,Mobile")] Employee employee)
		{
			if (id != employee.ID)
			{
				return NotFound();
			}

			ValidateModel(employee);

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(employee);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.Employees.Any(e => e.ID == employee.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			return View(employee);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Employees == null)
			{
				return NotFound();
			}

			var employee = await _context.Employees
				.Include(e => e.Manager)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Employees == null)
			{
				return Problem("Entity set 'HRContext.Employees'  is null.");
			}
			var employee = await _context.Employees.FindAsync(id);
			if (employee != null)
			{
				_context.Employees.Remove(employee);
			}

			await _context.Employees
				.Where(e => e.ManagerID == id)
				.ForEachAsync(e => e.ManagerID = null);

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private void ValidateModel(Employee employee)
		{
			if (!employee.ManagerID.HasValue)
				return;

			if (employee.ID == employee.ManagerID)
				ModelState.AddModelError(nameof(Employee.ManagerID), "Employee can't be a manager for themselves");

			if (!_context.Employees.Any(e => e.ID == employee.ManagerID))
				ModelState.AddModelError(nameof(Employee.ManagerID), $"{nameof(Employee.ManagerID)} corresponding employee not found");
		}
	}
}