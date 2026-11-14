namespace FraudOps.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Merchant { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsFlagged { get; set; }
    }
}
