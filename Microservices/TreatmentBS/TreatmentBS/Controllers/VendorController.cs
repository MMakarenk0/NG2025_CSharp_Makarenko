using DAL_Core.Enums;
using Microsoft.AspNetCore.Mvc;
using TreatmentBL.Models;
using TreatmentBL.Services.Interfaces;

namespace TreatmentBS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;
    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllVendors()
    {
        var vendors = await _vendorService.GetAllVendors();
        return Ok(vendors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVendorById(Guid id)
    {
        var vendor = await _vendorService.GetVendorById(id);
        if (vendor == null)
        {
            return NotFound();
        }
        return Ok(vendor);
    }

    [HttpGet("many")]
    public async Task<IActionResult> GetManyVendors([FromQuery] IEnumerable<Guid> ids)
    {
        var vendors = await _vendorService.GetManyVendorsByIds(ids);
        return Ok(vendors);
    }

    [HttpGet("contractType/{type}")]
    public async Task<IActionResult> GetVendorsByContractType(string type)
    {
        if (!Enum.TryParse(type, true, out ContractType contractType))
        {
            return BadRequest("Invalid contract type.");
        }
        var vendors = await _vendorService.GetVendorsByContractType(contractType);
        return Ok(vendors);
    }

    [HttpGet("{vendorId}/healthcares")]
    public async Task<IActionResult> GetVendorHealthCares(Guid vendorId)
    {
        var healthCares = await _vendorService.GetVendorHealthCares(vendorId);
        return Ok(healthCares);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVendor([FromBody] CreateVendorDto vendorDto)
    {
        if (vendorDto == null)
        {
            return BadRequest("Invalid vendor data.");
        }
        var vendorId = await _vendorService.CreateVendor(vendorDto);
        return CreatedAtAction(nameof(GetVendorById), new { id = vendorId }, vendorId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVendor([FromBody] CreateVendorDto vendorDto)
    {
        if (vendorDto == null)
        {
            return BadRequest("Invalid vendor data.");
        }
        var vendorId = await _vendorService.UpdateVendor(vendorDto);
        return Ok(vendorId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVendor(Guid id)
    {
        await _vendorService.DeleteVendor(id);
        return NoContent();
    }
}
