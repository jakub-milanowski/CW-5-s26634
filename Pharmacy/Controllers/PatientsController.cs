using Microsoft.AspNetCore.Mvc;
using Pharmacy.Services;

namespace Pharmacy.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController(IDBService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var patient = await service.GetPatientAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }
}