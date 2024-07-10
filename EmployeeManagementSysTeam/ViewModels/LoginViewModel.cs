using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSysTeam.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(100000000, 999999999, ErrorMessage = "Please enter a valid 10-digit phone number")]
        public int PhoneNumber { get; set; }
    }
}
