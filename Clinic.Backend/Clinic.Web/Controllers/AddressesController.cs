using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Addresses;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AddressResponse>> GetIdAdress(Guid id)
    {
        var result = await _addressService.GetByAddressId(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAddress([FromBody] AddressRequest request)
    {
        var res = Address.Create(
            Guid.NewGuid(),
            request.Country,
            request.Region,
            request.City,
            request.Street,
            request.HouseNumber,
            request.ApartmentNumber,
            request.Description,
            request.Pavilion);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        var addResult = await _addressService.AddAddress(res.Value);
        if (addResult.IsFailure)
        {
            return BadRequest(addResult.Error);
        }

        return Ok(res.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateAddress(Guid id, [FromBody] AddressRequest request)
    {
        var result = await _addressService.UpdateAddress(
            id,
            request.Country,
            request.Region,
            request.City,
            request.Street,
            request.HouseNumber,
            request.ApartmentNumber,
            request.Description,
            request.Pavilion);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteAddress(Guid id)
    {
        var result = await _addressService.DeleteAddress(id);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(id);
    }
}

