using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Employees.Models.DBModels.Entities
{
	public class Log
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LogID { get; set; }

		public int EmployeeID { get; set; }

		public DateTime DateTime { get; set; }

		public virtual Employee Employee { get; set; }
	}
}