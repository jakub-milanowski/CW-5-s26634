using Microsoft.AspNetCore.Mvc;
using Pharmacy.DTOs;
using Pharmacy.Exceptions;
using Pharmacy.Services;

namespace Pharmacy.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDBService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionPostDto postData)
    {
        try
        {
            var dto = await service.CreatePrescriptionAsync(postData);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}