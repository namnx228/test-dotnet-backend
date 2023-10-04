using AuthenticationServer.Models;
using AuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authenticatioService;

    public AuthenticationController(AuthenticationService authenticationService) =>
        _authenticatioService = authenticationService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _authenticatioService.GetAsync();

    // [HttpPost]
    // public async Task<IActionResult> Login()
    // {
    //     var user = await _authenticatioService.GetAsync("Trang", "password");
    //     System.Diagnostics.Debug.WriteLine($"User value is {user}");
    //     if (user is null)
    //     {
    //         return StatusCode(403);
    //     }
    //     System.Diagnostics.Debug.WriteLine($"User value is {user}");
    //     return Ok(user);
    // }

    [HttpPost]
    public async Task<IActionResult> Login(User cred)
    {
        var user = await _authenticatioService.GetAsync(cred.Username, cred.Password);
        System.Diagnostics.Debug.WriteLine($"User value is {user}");
        if (user is null)
        {
            return StatusCode(403);
        }
        System.Diagnostics.Debug.WriteLine($"User value is {user}");
        return Ok($"Welcome, {user.Username}");
    }
}