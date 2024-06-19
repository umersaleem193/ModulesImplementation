using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModulesImplementation.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int? ParentDepartmentId { get; set; }
        public IEnumerable<SelectListItem> ParentDepartments { get; set; } = new List<SelectListItem>();

    }
}
