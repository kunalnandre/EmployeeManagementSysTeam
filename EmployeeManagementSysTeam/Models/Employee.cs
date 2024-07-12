namespace EmployeesManagementSysTeam.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }        
        public string ProjectName { get; set; }
		public DateTime HireDate { get; set; } 
		public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
