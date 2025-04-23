using DAL_Core.Enums;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;

namespace PetAdoptionBS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;

    public PetController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPets()
    {
        var pets = await _petService.GetAllPetsAsync();
        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetById(Guid id)
    {
        var pet = await _petService.GetPetByIdAsync(id);
        if (pet == null)
        {
            return NotFound();
        }
        return Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet([FromForm] CreatePetDto pet)
    {
        if (pet == null)
        {
            return BadRequest("Pet cannot be null");
        }
        var result = await _petService.CreatePetAsync(pet);
        return CreatedAtAction(nameof(CreatePet), new { id = pet.Id }, pet);
    }

    [HttpPost]
    [Route("adopt")]
    public async Task<IActionResult> AdoptPet([FromForm] AdoptPetDto adoptPetDto)
    {
        if (adoptPetDto == null)
        {
            return BadRequest("AdoptPetDto cannot be null");
        }
        await _petService.AdoptPetAsync(adoptPetDto.PetId, adoptPetDto.CustomerId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(Guid id)
    {
        await _petService.DeletePetAsync(id);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePet([FromForm] CreatePetDto pet)
    {
        if (pet == null)
        {
            return BadRequest("Pet cannot be null");
        }
        var result = await _petService.UpdatePetAsync(pet);
        return Ok(result);
    }

    [HttpGet]
    [Route("type/{type}")]
    public async Task<IActionResult> GetPetsByType(PetTypes type)
    {
        var pets = await _petService.GetPetsByTypeAsync(type);
        return Ok(pets);
    }

    [HttpGet]
    [Route("store/{storeId}")]
    public async Task<IActionResult> GetAllPetsByStoreId(Guid storeId)
    {
        var pets = await _petService.GetAllPetsByStoreIdAsync(storeId);
        return Ok(pets);
    }
}
