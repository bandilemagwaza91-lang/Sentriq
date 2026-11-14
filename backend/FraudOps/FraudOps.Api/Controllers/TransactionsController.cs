using FraudOps.Api.DTOs;
using FraudOps.Api.Models;
using FraudOps.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FraudOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IFraudDetectionService _fraudService;

    public TransactionsController(IFraudDetectionService fraudService)
    {
        _fraudService = fraudService;
    }

    [HttpPost]
    public ActionResult<TransactionResponse> Create(CreateTransactionRequest request)
    {
        var transaction = _fraudService.ProcessTransaction(request);
        return Ok(ToResponse(transaction));
    }

    [HttpGet]
    public ActionResult<IEnumerable<TransactionResponse>> GetAll()
    {
        var transactions = _fraudService.GetTransactions().Select(ToResponse);
        return Ok(transactions);
    }

    private static TransactionResponse ToResponse(Transaction t) => new()
    {
        Id = t.Id,
        AccountId = t.AccountId,
        Amount = t.Amount,
        Merchant = t.Merchant,
        Location = t.Location,
        Timestamp = t.Timestamp,
        IsFlagged = t.IsFlagged
    };
}
