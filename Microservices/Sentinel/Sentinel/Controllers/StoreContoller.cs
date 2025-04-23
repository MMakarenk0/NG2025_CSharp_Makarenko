using Microsoft.AspNetCore.Mvc;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreContoller : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreContoller(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStores()
    {
        var stores = await _storeService.GetAllStoresAsync();
        return Ok(stores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreById(Guid id)
    {
        var store = await _storeService.GetStoreByIdAsync(id);
        return Ok(store);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto dto)
    {
        await _storeService.CreateStoreAsync(dto);
        return CreatedAtAction(nameof(GetStoreById), new { id = dto.Id }, dto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStore([FromBody] CreateStoreDto dto)
    {
        await _storeService.UpdateStoreAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(Guid id)
    {
        await _storeService.DeleteStoreAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/pets")]
    public async Task<IActionResult> GetStorePets(Guid id)
    {
        var pets = await _storeService.GetPetsByStoreIdAsync(id);
        return Ok(pets);
    }

    [HttpGet("{id}/pets/treatments")]
    public async Task<IActionResult> GetStorePetsTreatments(Guid id)
    {
        var treatments = await _storeService.GetPetsTreatmentsByStoreIdAsync(id);
        return Ok(treatments);
    }
}
