using Microsoft.AspNetCore.Mvc;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;

namespace PetAdoptionBS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
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
        if (store == null)
        {
            return NotFound();
        }
        return Ok(store);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto storeDto)
    {
        if (storeDto == null)
        {
            return BadRequest("Store cannot be null");
        }
        var result = await _storeService.CreateStoreAsync(storeDto);
        return CreatedAtAction(nameof(GetStoreById), new { id = result }, storeDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStore([FromBody] CreateStoreDto storeDto)
    {
        if (storeDto == null)
        {
            return BadRequest("Store cannot be null");
        }
        var result =  await _storeService.UpdateStoreAsync(storeDto);
        return Ok(result);
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
        var pets = await _storeService.GetStorePetsAsync(id);
        return Ok(pets);
    }

    [HttpGet("{id}/pets/treatments")]
    public async Task<IActionResult> GetStorePetsTreatments(Guid id)
    {
        var treatments = await _storeService.GetStorePetsTreatmentsAsync(id);
        return Ok(treatments);
    }
}
