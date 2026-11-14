using FraudOps.Api.DTOs;
using FraudOps.Api.Models;

namespace FraudOps.Api.Services
{
    public class FraudDetectionService : IFraudDetectionService
    {
        private const decimal LargeAmountThreshold = 10000m;
        private const int RapidTransactionCount = 5;
        private static readonly TimeSpan RapidWindow = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan LocationWindow = TimeSpan.FromMinutes(10);

        private readonly List<Transaction> _transactions = new();
        private readonly List<Alert> _alerts = new();
        private readonly object _lock = new();
        private int _nextTransactionId = 1;
        private int _nextAlertId = 1;
        public Transaction ProcessTransaction(CreateTransactionRequest request)
        {
            lock (_lock)
            {
                var transaction = new Transaction
                {
                    Id = _nextTransactionId++,
                    AccountId = request.AccountId,
                    Amount = request.Amount,
                    Merchant = request.Merchant,
                    Location = request.Location,
                    Timestamp = DateTime.UtcNow
                };

                var findings = new List<(string Reason, string Severity)>();

                // Rule 1: checks incase of large amounts
                if (transaction.Amount > LargeAmountThreshold)
                {
                    findings.Add((
                        $"Large amount: {transaction.Amount:N2} is over the {LargeAmountThreshold:N2} limit",
                        "High"));
                }

                var previous = _transactions
                    .Where(t => t.AccountId == transaction.AccountId)
                    .ToList();

                // Rule 2: this will detect if the user has multiple transaction in a short amount of time 
                var recentCount = previous.Count(t => transaction.Timestamp - t.Timestamp <= RapidWindow);
                if (recentCount + 1 >= RapidTransactionCount)
                {
                    findings.Add((
                        $"Rapid activity: {recentCount + 1} transactions within 1 minute",
                        "Medium"));
                }

                // Rule 3: frequently changing locations 
                var last = previous.LastOrDefault();
                if (last != null
                    && !string.Equals(last.Location, transaction.Location, StringComparison.OrdinalIgnoreCase)
                    && transaction.Timestamp - last.Timestamp <= LocationWindow)
                {
                    findings.Add((
                        $"Location change: {last.Location} to {transaction.Location} within 10 minutes",
                        "Medium"));
                }

                transaction.IsFlagged = findings.Count > 0;
                _transactions.Add(transaction);

                foreach (var (reason, severity) in findings)
                {
                    _alerts.Add(new Alert
                    {
                        Id = _nextAlertId++,
                        TransactionId = transaction.Id,
                        Reason = reason,
                        Severity = severity,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                return transaction;
            }
        }

        public IReadOnlyList<Transaction> GetTransactions()
        {
            lock (_lock)
            {
                return _transactions.ToList();
            }
        }

        public IReadOnlyList<Alert> GetAlerts()
        {
            lock (_lock)
            {
                return _alerts.ToList();
            }
        }
    }
}
