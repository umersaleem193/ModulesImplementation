using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModulesImplementation.Data;
using ModulesImplementation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ModulesImplementation.Controllers
{
    public class DepartmentHierarchyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentHierarchyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Linq query is applied here to get the sub departments which are linked to a main department
            var hierarchyVM = _context.DepartmentsHierarchy
                .Join(_context.SubDepartments, h => h.SubDepartmentId, s => s.Id, (h, s) => new { h, s })
                .Join(_context.Departments, h => h.h.DepartmentId, d => d.Id, (h, d) => new { h, d })
                .Select(x => new DepartmentHierarchyViewModel()
                {
                    Id = x.h.h.Id,
                    Name = x.h.h.Name,
                    DepartmentId = x.d.Id,
                    SubDepartmentId = x.h.s.Id,
                    DepartmentName = x.d.Name,
                    SubDepartmentName = x.h.s.Name
                });


            return View(hierarchyVM);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new DepartmentHierarchyViewModel
            {
                Departments = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList(),

                SubDepartments = _context.SubDepartments.Select(sd => new SelectListItem
                {
                    Value = sd.Id.ToString(),
                    Text = sd.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentHierarchyViewModel viewModel)
        {
            
                var departmentHierarchy = new DepartmentHierarchy
                {
                    Name = viewModel.Name,
                    DepartmentId = viewModel.DepartmentId,
                    SubDepartmentId = viewModel.SubDepartmentId
                };

                _context.DepartmentsHierarchy.Add(departmentHierarchy);
                await _context.SaveChangesAsync();
    
            var hierarchyVM = _context.DepartmentsHierarchy
    .Join(_context.SubDepartments, h => h.SubDepartmentId, s => s.Id, (h, s) => new { h, s })
    .Join(_context.Departments, h => h.h.DepartmentId, d => d.Id, (h, d) => new { h, d })
    .Select(x => new DepartmentHierarchyViewModel()
    {
        Id = x.h.h.Id,
        Name = x.h.h.Name,
        DepartmentId = x.d.Id,
        SubDepartmentId = x.h.s.Id,
        DepartmentName = x.d.Name,
        SubDepartmentName = x.h.s.Name
    });


            return View("../DepartmentHierarchy/Index", hierarchyVM);

        }
    }
}
