namespace PermissionHub.Models
{

    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CompanyId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
