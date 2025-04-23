using Microsoft.AspNetCore.Mvc;
using PetAdoptionBL.Models;
using PetAdoptionBL.Services.Interfaces;

namespace PetAdoptionBS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
    {
        if (customerDto == null)
        {
            return BadRequest("Customer cannot be null");
        }
        var result = await _customerService.CreateCustomerAsync(customerDto);
        return CreatedAtAction(nameof(GetCustomerById), new { id = result }, customerDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] CreateCustomerDto customerDto)
    {
        if (customerDto == null)
        {
            return BadRequest("Customer cannot be null");
        }
        await _customerService.UpdateCustomerAsync(customerDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return NoContent();
    }
}
