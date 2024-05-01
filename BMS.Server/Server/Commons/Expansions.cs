using System.Security.Claims;
using Masuit.Tools.Mime;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Server.Commons;

public static class Expansions
{

	#region Claims Principal Expansions

	public static IEnumerable<string> FindAllValue(this ClaimsPrincipal user, string type) =>
		user.FindAll(type).Select(it => it.Value);

	#endregion

	#region Cookie Authorize Actions

	public static async Task OnRedirectToAccessDenied(
		RedirectContext<CookieAuthenticationOptions> context
	)
	{
		context.Response.StatusCode = StatusCodes.Status403Forbidden;
		context.Response.ContentType = MimeMapper.MimeTypes[".json"];
		await context.Response.WriteAsync(new { Msg = "未授权！" }.ToJsonString());
	}

	public static async Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
	{
		context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		context.Response.ContentType = MimeMapper.MimeTypes[".json"];
		await context.Response.WriteAsync(new { Msg = "未登录！" }.ToJsonString());
	}

	#endregion

	#region SqlSugar Expansions

	#endregion
}
