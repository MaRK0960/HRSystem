using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Employees.Models.DBModels.Entities
{
	public class Employee
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		public int ID { get; set; }

		[DataType(DataType.Text)]
		public int? ManagerID { get; set; }

		[Required]
		[RegularExpression(@"[a-zA-Z \-.]+", ErrorMessage = "Name must be valid format")]
		public string Name { get; set; }

		[Required]
		public string Address { get; set; }

		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid Email address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"0\d{10}", ErrorMessage = "Mobile number must be only 11 numbers starting with 0")]
		public string Mobile { get; set; }

		public virtual Employee Manager { get; set; }

		public virtual ICollection<Log> Logs { get; set; }
	}
}