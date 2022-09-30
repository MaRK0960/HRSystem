using HR_Employees.Models.DBModels.Entities;

namespace HR_Employees.Models.DBModels
{
	public static class DbInitializer
	{
		public static void Initialize(HRContext context)
		{
			Employee[] employees = new Employee[]
			{
				new()
				{
					ID = 6425,
					ManagerID = 9563,
					Name = "Ahmed",
					Address = "Ismailia",
					BirthDate = new(1996, 4, 26),
					Email = "ahmed@hrsystem.com",
					Mobile = "01512645865",
				},
				new()
				{
					ID = 9563,
					ManagerID  = 1587,
					Name = "Hesham",
					Address = "Ismailia",
					BirthDate = new(1966, 6, 16),
					Email = "hesham@hrsystem.com",
					Mobile = "01122645865",
				},
				new()
				{
					ID = 1587,
					ManagerID = 7563,
					Name = "Abdalla",
					Address = "Portsaid",
					BirthDate = new(1986, 5, 16),
					Email = "abdalla@hrsystem.com",
					Mobile = "01112645965",
				},
				new()
				{
					ID = 7563,
					Name = "Waleed",
					Address = "Cairo",
					BirthDate = new(1992, 4, 6),
					Email = "waleed@hrsystem.com",
					Mobile = "01118645865",
				},
				new()
				{
					ID = 4953,
					Name = "Mohammed",
					Address = "Cairo",
					BirthDate = new(1975, 8, 2),
					Email = "mohammed@hrsystem.com",
					Mobile = "01112645865",
				}
			};

			context.Employees.AddRange(employees);
			context.SaveChanges();

			List<Log> logs = new();
			foreach (int employeeID in employees.Select(e => e.ID))
			{
				DateTime startDay = DateTime.Today;
				Random random = new();
				for (int i = 0; i < 10; i++)
				{
					if (random.Next(2) == 1)
						continue;

					DateTime day = startDay.AddDays(i);
					DateTime login = day.AddHours(random.Next(7, 9)).AddMinutes(random.Next(0, 59));
					DateTime logout = day.AddHours(random.Next(15, 16)).AddMinutes(random.Next(0, 59));

					logs.Add(new() { EmployeeID = employeeID, DateTime = login, IsIn = true });
					logs.Add(new() { EmployeeID = employeeID, DateTime = logout, IsIn = false });
				}
			}

			context.Logs.AddRange(logs);
			context.SaveChanges();
		}
	}
}