using Azure;
using Clinic.Application.Services;
using Clinic.Core.Interfaces.Services;
using Clinic.Web.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly string _imagesPath =
        Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles/Images");

    private readonly IUserService _userService;
    private readonly IImageService _imageService;

    public UsersController(IUserService userService, IImageService imageService)
    {
        _userService = userService;
        _imageService = imageService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
    {
        var result = await _userService.Register(
            request.FirstName,
            request.LastName,
            request.FatherName,
            request.DateOfBirth,
            request.Email,
            request.PhoneNumber,
            request.Password);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        var result = await _userService.Login(request.Email, request.Password);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        Response.Cookies.Append("secretCookie", result.Value);

        return Ok(result.Value);
    }

    [HttpPost("updateProfile")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
    {
        Guid? imageId = null;
        if (request.FileName is not null)
        {
            var imageResult = await _imageService.AddImage(request.FileName, _imagesPath);

            if (imageResult.IsFailure)
            {
                return BadRequest(imageResult.Error);
            }

            imageId = imageResult.Value.Id;
        }

        var result = await _userService.Update(
           id,
           request.FirstName,
           request.LastName,
           request.FatherName,
           request.DateOfBirth,
           request.AddressId,
           imageId);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}