namespace PermissionHub.Models
{

    public class UserRole
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public Guid CompanyId { get; set;  }
    }
}
