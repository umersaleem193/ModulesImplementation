using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModulesImplementation.Data;
using ModulesImplementation.Models;
using Microsoft.EntityFrameworkCore;


namespace ModulesImplementation.Controllers
{
    public class SubDepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubDepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new SubDepartmentViewModel
            {
                Departments = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubDepartmentViewModel viewModel)
        {

            var subDepartment = new SubDepartment
            {
                Name = viewModel.Name,
                Logo = viewModel.Logo,
                DepartmentId = viewModel.DepartmentId
            };

            _context.SubDepartments.Add(subDepartment);
            await _context.SaveChangesAsync();




            var subDepartments = await _context.SubDepartments.Join(_context.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { s, d })
    .Select(x => new SubDepartmentViewModel
    {
        Id = x.s.Id,
        Name = x.s.Name,
        Logo = x.s.Logo,
        DepartmentName = x.d.Name
    }).ToListAsync();



            return View("../SubDepartment/Index", subDepartments);
        }

        public async Task<IActionResult> Index()
        {
            var subDepartments = await _context.SubDepartments.Join(_context.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { s, d })
                .Select(x => new SubDepartmentViewModel
                {
                    Id = x.s.Id,
                    Name = x.s.Name,
                    Logo = x.s.Logo,
                    DepartmentName = x.d.Name
                }).ToListAsync();


            return View(subDepartments);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var subDepartmentViewModel = await _context.SubDepartments.Join(_context.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { s, d })
                .Select(x => new SubDepartmentViewModel()
                {
                    Id = x.s.Id,
                    Name = x.s.Name,
                    Logo = x.s.Logo,
                    DepartmentId = x.d.Id, 
                    DepartmentName = x.d.Name,
                    Departments = new SelectList(_context.Departments, "Id", "Name")

                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (subDepartmentViewModel == null)
            {
                return NotFound();
            }


            return View(subDepartmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo,DepartmentId")] SubDepartment subDepartment)
        {
            if (id != subDepartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubDepartmentExists(subDepartment.Id))
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
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", subDepartment.DepartmentId);
            return View(subDepartment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subDepartment = await _context.SubDepartments
                .FirstOrDefaultAsync(m => m.Id == id);

            return View("../SubDepartment/Delete", subDepartment);

        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subDepartment = await _context.SubDepartments.FindAsync(id);
            _context.SubDepartments.Remove(subDepartment);
            await _context.SaveChangesAsync();

            var subDepartments = await _context.SubDepartments.Join(_context.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { s, d })
    .Select(x => new SubDepartmentViewModel
    {
        Id = x.s.Id,
        Name = x.s.Name,
        Logo = x.s.Logo,
        DepartmentName = x.d.Name
    }).ToListAsync();



            return View("../SubDepartment/Index", subDepartments);
        }

        private bool SubDepartmentExists(int id)
        {
            return _context.SubDepartments.Any(e => e.Id == id);
        }
    }

}

