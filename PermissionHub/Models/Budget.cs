namespace PermissionHub.Models
{
    public class BudgetModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public string Title { get; set; } = "";
        public decimal Amount { get; set; }
    }
}
