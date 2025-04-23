using Microsoft.AspNetCore.Mvc;
using TreatmentBL.Models;
using TreatmentBL.Services.Interfaces;

namespace TreatmentBS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TreatmentController : ControllerBase
{
    private readonly ITreatmentService _treatmentService;

    public TreatmentController(ITreatmentService treatmentService)
    {
        _treatmentService = treatmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTreatments()
    {
        var treatments = await _treatmentService.GetAllTreatments();

        return Ok(treatments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTreatmentById(Guid id)
    {
        var treatment = await _treatmentService.GetTreatment(id);
        if (treatment == null)
        {
            return NotFound();
        }
        return Ok(treatment);
    }

    [HttpGet("status/{id}")]
    public async Task<IActionResult> GetTreatmentStatus(Guid id)
    {
        var isExpired = await _treatmentService.GetTreatmentStatus(id);
        return Ok(isExpired);
    }

    [HttpGet("many")]
    public async Task<IActionResult> GetManyTreatments([FromQuery] IEnumerable<Guid> ids)
    {
        var treatments = await _treatmentService.GetManyTreatmentByIds(ids);
        return Ok(treatments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTreatment([FromForm] CreateTreatmentDto treatmentDto)
    {
        if (treatmentDto == null)
        {
            return BadRequest("Invalid treatment data.");
        }
        var treatmentId = await _treatmentService.CreateTreatment(treatmentDto);
        return CreatedAtAction(nameof(GetTreatmentById), new { id = treatmentId }, treatmentId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTreatment([FromForm] CreateTreatmentDto treatmentDto)
    {
        if (treatmentDto == null)
        {
            return BadRequest("Invalid treatment data.");
        }
        var treatmentId = await _treatmentService.UpdateTreatment(treatmentDto);
        return Ok(treatmentId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTreatment(Guid id)
    {
        var treatment = await _treatmentService.GetTreatment(id);
        if (treatment == null)
        {
            return NotFound();
        }
        await _treatmentService.DeleteTreatment(id);
        return NoContent();
    }
}
