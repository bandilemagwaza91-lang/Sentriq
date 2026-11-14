namespace FraudOps.Api.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Severity { get; set; } = "Medium";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
