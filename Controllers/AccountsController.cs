// check CI 2
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualBasic;
using Serilog;

[Route("web-api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtTokenService _jwtTokenService;

    public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
                              , JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = new IdentityUser();
        try
        {
            user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

        }
        catch (Exception e)
        {
            Log.Error(e, e.Message);
        }
        var roles = await _userManager.GetRolesAsync(user!);
        var token = _jwtTokenService.GenerateToken(user!, roles);
        return Ok(new { Token = token });
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
