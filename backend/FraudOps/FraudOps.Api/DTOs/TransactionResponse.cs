namespace FraudOps.Api.DTOs
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Merchant { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsFlagged
        {
            get; set;
        }
    }
}
