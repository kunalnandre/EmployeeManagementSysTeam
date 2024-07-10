using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSysTeam.Context;
using EmployeeManagementSysTeam.ViewModels;
using System.Threading.Tasks;

namespace EmployeeManagementSysTeam.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
            
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Username == model.Username && a.Password == model.Password);

                if (admin != null)  
                {
                    // Redirect to the dashboard if login is successful
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    // Show a warning if username or password is wrong
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }
    }
}
