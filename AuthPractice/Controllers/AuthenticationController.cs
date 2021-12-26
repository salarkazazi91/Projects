using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public UserManager<IdentityUser> userManager { get; set; }
    public SignInManager<IdentityUser> signInManager { get; set; }

    public AuthenticationController(
        SignInManager<IdentityUser> signInManager,
         UserManager<IdentityUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(string username, string password)
    {

        var user = await userManager.FindByNameAsync(username);
        if (user is not null)
        {
            await signInManager.SignInAsync(user, false, "kiss my ass");
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(string username, string password)
    {

        var user = new IdentityUser
        {
            UserName = username,
            Email = username
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false, "kiss my ass");
            return Ok();
        }


        return BadRequest(result.Errors);
    }
}