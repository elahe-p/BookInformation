using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookInformation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditLogsController : ControllerBase
{
    private readonly IAuditLogService _service;

    public AuditLogsController(IAuditLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AuditLogQueryDto dto)
    {
        var result = await _service.GetAsync(dto);
        return Ok(result);
    }
}
