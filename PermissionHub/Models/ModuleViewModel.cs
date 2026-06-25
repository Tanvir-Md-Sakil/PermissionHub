namespace PermissionHub.Models
{
    public class ModuleViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Code { get; set; } = "";

        public bool IsAssigned { get; set; }
    }
}
