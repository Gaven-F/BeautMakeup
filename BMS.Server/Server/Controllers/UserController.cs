using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Server.Controllers;

public class UserController : ApiController
{
	[AllowAnyone, HttpGet]
	public async Task<ActionResult> Demo(string key)
	{
		if (key == "GavenF")
		{
			var claims = new List<Claim> { new(CT.Name, "GavenF"), new(CT.Role, "GavenF"), new(CT.Role, "Admin ") };

			var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authenticationProperties = new AuthenticationProperties { };


			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimIdentity),
				authenticationProperties);

			return base.Ok(new { Data = User.FindAllValue(CT.Role) });
		}
		return StatusCode(StatusCodes.Status401Unauthorized);
	}

	[Authorize(Roles = "GavenF")]
	public ActionResult AdminApi()
	{
		return new OkResult();
	}



}
