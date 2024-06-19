using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModulesImplementation.Models
{
    public class DepartmentHierarchyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int SubDepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string SubDepartmentName { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> SubDepartments { get; set; } = new List<SelectListItem>();
    }
}
