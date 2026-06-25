namespace PermissionHub.Models
{
    public class CompanyModule
    {
        public Guid CompanyId { get; set; }
        public Guid ModuleId { get; set; }
        public bool IsEnabled { get; set; }

        public string CompanyName { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
    }
}
