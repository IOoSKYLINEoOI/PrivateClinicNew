//using Clinic.Application.Services;
//using Clinic.Core.Models;
//using Clinic.Web.Contracts.Addresses;
//using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("[controller]")]
//public class AddressesController : ControllerBase
//{
//    private readonly IAddressService _addressService;

//    public AddressesController(IAddressService addressService)
//    {
//        _addressService = addressService;
//    }

//    [HttpGet("{id:guid}")]
//    public async Task<ActionResult<AddressResponse>> GetIdAdress(Guid id)
//    {
//        var result = await _addressService.GetByAddressId(id);

//        if (result.IsFailure)
//        {
//            return NotFound(result.Error);
//        }

//        return Ok(result.Value);
//    }

//    [HttpPost]
//    public async Task<ActionResult> CreateAddress([FromBody] AddressRequest request)
//    {
//        var res = Address.Create(
//            Guid.NewGuid(),
//            request.Country,
//            request.Region,
//            request.City,
//            request.Street,
//            request.HouseNumber,
//            request.ApartmentNumber,
//            request.Description,
//            request.Pavilion);

//        if (res.IsFailure)
//        {
//            return BadRequest(res.Error);
//        }

//        var addResult = await _addressService.AddAddress(res.Value);
//        if (addResult.IsFailure)
//        {
//            return BadRequest(addResult.Error);
//        }

//        return Ok(res.Value);
//    }

//    [HttpPut("{id:guid}")]
//    public async Task<ActionResult<Guid>> UpdateAddress(Guid id, [FromBody] AddressRequest request)
//    {
//        var result = await _addressService.UpdateAddress(
//            id,
//            request.Country,
//            request.Region,
//            request.City,
//            request.Street,
//            request.HouseNumber,
//            request.ApartmentNumber,
//            request.Description,
//            request.Pavilion);

//        if (result.IsFailure)
//        {
//            return BadRequest(result.Error);
//        }

//        return Ok(id);
//    }

//    [HttpDelete("{id:guid}")]
//    public async Task<ActionResult<Guid>> DeleteAddress(Guid id)
//    {
//        var result = await _addressService.DeleteAddress(id);
//        if (result.IsFailure)
//        {
//            return BadRequest(result.Error);
//        }

//        return Ok(id);
//    }
//}

using Clinic.Application.Services;
using Clinic.Core.Models;
using Clinic.Web.Contracts.Addresses;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AddressesController : Controller
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _addressService.GetByAddressId(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return View(result.Value);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] AddressRequest request)
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

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _addressService.GetByAddressId(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        var address = result.Value;
        var model = new AddressRequest(
            address.Country,
            address.Region,
            address.City,
            address.Street,
            address.HouseNumber,
            address.ApartmentNumber,
            address.Description,
            address.Pavilion);

        return View(model);
    }

    [HttpPost("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] AddressRequest request)
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

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _addressService.GetByAddressId(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return View(result.Value);
    }

    [HttpPost("delete/{id:guid}")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var result = await _addressService.DeleteAddress(id);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return RedirectToAction(nameof(Index));
    }
};