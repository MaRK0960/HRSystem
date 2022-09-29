using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Employees.Models.DBModels
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey(nameof(Manager))]
        public int? ManagerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string Email { get; set; }

        public int Mobile { get; set; }

        public virtual Employee Manager { get; set; }

        public ICollection<Log> Logs { get; set; }
    }
}