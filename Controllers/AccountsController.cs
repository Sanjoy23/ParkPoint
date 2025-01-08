using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualBasic;

[Route("web-api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (ModelState.IsValid)
        {

        }
        return Ok();
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userExist = await _userManager.FindByEmailAsync(model.Email ?? string.Empty);
        if (userExist != null)
        {
            return BadRequest(new { Message = "Email is already in use." });
        }

        var user = new AppUser
        {
            UserName = model.Email,
            FullName = model.FullName,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        if (!await _roleManager.RoleExistsAsync(model.Roles))
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Roles));
        }
        
        await _userManager.AddToRoleAsync(user, model.Roles);
        return Ok(new { Message = "User Registered successfullly" });
    }


}