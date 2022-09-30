using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Employees.Models.DBModels
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ManagerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}