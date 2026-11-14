using System.ComponentModel.DataAnnotations;

namespace FraudOps.Api.DTOs
{
    public class CreateTransactionRequest
    {
        [Required]
        public string AccountId { get; set; } = string.Empty;

        [Range(0.01, 100000000)]
        public decimal Amount { get; set; }

        [Required]
        public string Merchant { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;
    }
}
