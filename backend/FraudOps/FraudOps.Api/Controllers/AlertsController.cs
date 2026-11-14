using FraudOps.Api.Models;
using FraudOps.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FraudOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly IFraudDetectionService _fraudService;

    public AlertsController(IFraudDetectionService fraudService)
    {
        _fraudService = fraudService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Alert>> GetAll()
    {
        return Ok(_fraudService.GetAlerts());
    }
}
