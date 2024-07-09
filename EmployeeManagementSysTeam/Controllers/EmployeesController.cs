using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
// using EmployeesManagementSysTeam.Context;
using EmployeesManagementSysTeam.Models;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSysTeam.Context;

namespace EmployeesManagementSysTeam.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

            public IActionResult EmployeeLoginPage()
            {
                return View();
            }

        public IActionResult AdminLoginPage()
        {
            return View();
        }



        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(e => e.Projects).ToListAsync();
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();  
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.Projects = new SelectList(_context.Projects, "Name", "Name");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,EmailAddress,PhoneNumber,ProjectName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (_context.Employees.Any(e => e.EmailAddress == employee.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Email address is already registered.");
                }

                if (_context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number is already registered.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Projects = new SelectList(_context.Projects, "Name", "Name", employee.ProjectName);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Projects, "Name", "Name", employee.ProjectName);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,EmailAddress,PhoneNumber,ProjectName")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Employees.Any(e => e.EmailAddress == employee.EmailAddress && e.Id != employee.Id))
                {
                    ModelState.AddModelError("EmailAddress", "Email address is already registered.");
                }

                if (_context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber && e.Id != employee.Id))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number is already registered.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(employee.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Projects = new SelectList(_context.Projects, "Name", "Name", employee.ProjectName);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Projects)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public class CheckEmailPhoneResult
        {
            public bool EmailRegistered { get; set; }
            public bool PhoneRegistered { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailOrPhoneRegistered(string email, int phone, int? id)
        {
            int employeeId = id.HasValue ? id.Value : 0;

            var emailRegistered = await _context.Employees.AnyAsync(e => e.EmailAddress == email && (id == null || e.Id != employeeId));
            var phoneRegistered = await _context.Employees.AnyAsync(e => e.PhoneNumber == phone && (id == null || e.Id != employeeId));

            return Json(new CheckEmailPhoneResult
            {
                EmailRegistered = emailRegistered,
                PhoneRegistered = phoneRegistered
            });
        }
    }
}
