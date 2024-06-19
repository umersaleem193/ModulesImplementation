namespace ModulesImplementation.Models
{
    public class DepartmentHierarchy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int SubDepartmentId { get; set; }

    }
}
