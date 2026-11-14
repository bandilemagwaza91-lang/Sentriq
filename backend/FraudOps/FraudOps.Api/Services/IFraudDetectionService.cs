using FraudOps.Api.DTOs;
using FraudOps.Api.Models;

namespace FraudOps.Api.Services
{
    public interface  IFraudDetectionService
    {
        Transaction ProcessTransaction(CreateTransactionRequest request);
        IReadOnlyList<Transaction> GetTransactions();
        IReadOnlyList<Alert> GetAlerts();
    }
}
