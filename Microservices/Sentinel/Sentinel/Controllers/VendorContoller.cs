using Microsoft.AspNetCore.Mvc;
using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;

namespace Sentinel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorContoller : ControllerBase
{
    private readonly IVendorClient _vendorClient;
    public VendorContoller(IVendorClient vendorClient)
    {
        _vendorClient = vendorClient;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllVendors()
    {
        var vendors = await _vendorClient.GetAllVendorsAsync();
        return Ok(vendors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVendorById(Guid id)
    {
        var vendor = await _vendorClient.GetVendorByIdAsync(id);
        return Ok(vendor);
    }
    [HttpPost]
    public async Task<IActionResult> CreateVendor([FromBody] CreateVendorDto dto)
    {
        await _vendorClient.CreateVendorAsync(dto);
        return CreatedAtAction(nameof(GetVendorById), new { id = dto.Id }, dto);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateVendor([FromBody] CreateVendorDto dto)
    {
        await _vendorClient.UpdateVendorAsync(dto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVendor(Guid id)
    {
        await _vendorClient.DeleteVendorAsync(id);
        return NoContent();
    }
}
