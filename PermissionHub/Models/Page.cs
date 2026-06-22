namespace PermissionHub.Models
{

    public class Page
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
