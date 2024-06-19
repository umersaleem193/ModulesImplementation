using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModulesImplementation.Models
{
    public class SubDepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; } = new List<SelectListItem>();

    }
}
