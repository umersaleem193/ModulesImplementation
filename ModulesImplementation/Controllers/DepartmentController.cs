using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModulesImplementation.Data;
using ModulesImplementation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ModulesImplementation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new DepartmentViewModel
            {
                ParentDepartments = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Name = viewModel.Name,
                    Logo = viewModel.Logo,
                };

                _context.Add(department);
                await _context.SaveChangesAsync();
            }
            var departments = await _context.Departments
    .ToListAsync();
            return View("../Department/Index", departments);

        
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .ToListAsync();
            return View(departments);
        }

        [HttpGet("{id}/subdepartments")]
        public async Task<IActionResult> GetSubDepartments(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            var departments = await _context.Departments
    .ToListAsync();
            return View("../Department/Index", departments);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            var departments = await _context.Departments.ToListAsync();
            return View("../Department/Index", departments);

        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

    }
}