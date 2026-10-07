using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet("role-based")]
    public IActionResult GetUserByRole()
    {
        // Simulate a logged-in user with a role
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin") // Simulating an Admin role
        }, "mock"));

        HttpContext.User = user;

        // Perform role-based authorization manually
        if (user.IsInRole("Admin"))
        {
            return Ok(new { Message = "Access granted for Admin role." });
        }
        else
        {
            return Forbid();
        }
    }

    [HttpGet("claims-based")]
    public IActionResult GetUserByClaim()
    {
        // Simulate a logged-in user with a claim
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim("Department", "IT") // Simulating an IT Department claim
        }, "mock"));

        HttpContext.User = user;

        // Perform claim-based authorization manually
        var hasClaim = user.HasClaim(c => c.Type == "Department" && c.Value == "IT");

        if (hasClaim)
        {
            return Ok(new { Message = "Access granted for IT department." });
        }
        else
        {
            return Forbid();
        }
    }
}