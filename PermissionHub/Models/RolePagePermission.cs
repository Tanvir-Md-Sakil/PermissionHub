namespace PermissionHub.Models
{
    public class RolePagePermission
    {
        public Guid RoleId { get; set; }
        public string ModuleCode { get; set; }
        public string PageCode { get; set; }
        public bool CanAccess { get; set; }
    }
}
