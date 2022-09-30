namespace HR_Employees.Models.BusinessModels
{
    public class DayLog
    {
        public string EmployeeName { get; set; }
        public DateTime SignIn { get; set; }
        public DateTime SignOut { get; set; }
        public TimeSpan TimeWorked => SignOut - SignIn;
    }
}