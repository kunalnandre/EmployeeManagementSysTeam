using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSysTeam.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
