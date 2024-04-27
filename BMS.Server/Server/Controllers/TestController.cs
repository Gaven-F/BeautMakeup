using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Server.Models;
using System.Security.Claims;

namespace Server.Controllers;

[Route("[controller]/[action]")]
public class TestController() : ControllerBase
{
	private readonly List<TestModel> data =
	[
		new TestModel { Data = "Data01", Name = "Gaven", Extension = "Extension01", TilTime = "TilTime01", Id = 0 },
		new TestModel { Data = "Data02", Name = "Gaven", Extension = "Extension02", TilTime = "TilTime02", Id = 1 },
		new TestModel { Data = "Data03", Name = "Gaven", Extension = "Extension03", TilTime = "TilTime03", Id = 2 },
		new TestModel { Data = "Data04", Name = "Gaven", Extension = "Extension04", TilTime = "TilTime04", Id = 3 },
		new TestModel { Data = "Data05", Name = "Gaven", Extension = "Extension05", TilTime = "TilTime05", Id = 4 },
		new TestModel { Data = "Data06", Name = "Gaven", Extension = "Extension06", TilTime = "TilTime06", Id = 5 },
		new TestModel { Data = "Data07", Name = "Gaven", Extension = "Extension07", TilTime = "TilTime07", Id = 6 },
		new TestModel { Data = "Data08", Name = "Gaven", Extension = "Extension08", TilTime = "TilTime08", Id = 7 },
		new TestModel { Data = "Data09", Name = "Gaven", Extension = "Extension09", TilTime = "TilTime09", Id = 8 },
		new TestModel { Data = "Data10", Name = "Gaven", Extension = "Extension10", TilTime = "TilTime10", Id = 9 },
		new TestModel { Data = "Data11", Name = "Gaven", Extension = "Extension11", TilTime = "TilTime11", Id = 10 },
		new TestModel { Data = "Data12", Name = "Gaven", Extension = "Extension12", TilTime = "TilTime12", Id = 11 },
		new TestModel { Data = "Data13", Name = "Gaven", Extension = "Extension13", TilTime = "TilTime13", Id = 12 },
		new TestModel { Data = "Data14", Name = "Gaven", Extension = "Extension14", TilTime = "TilTime14", Id = 13 },
		new TestModel { Data = "Data15", Name = "Gaven", Extension = "Extension15", TilTime = "TilTime15", Id = 14 },
	];

	[EnableQuery(PageSize = 2)]
	public IActionResult Get() { return new OkObjectResult(data); }

	[AllowAnonymous]
	public async Task<IActionResult> Login()
	{
		var claims = new List<Claim> { new(ClaimTypes.Name, "GavenF") };

		var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var authenticationProperties = new AuthenticationProperties
		{
			//IsPersistent = true,
			//ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(30),
			//AllowRefresh = true,
		};


		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimIdentity),
			authenticationProperties);

		return new OkResult();
	}

	[Authorize]
	public ActionResult Demo()
	{
		return Ok(User.FindFirstValue(ClaimTypes.Name));
	}


}
