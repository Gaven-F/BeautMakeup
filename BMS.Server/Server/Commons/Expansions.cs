using System.Security.Claims;

namespace Server.Commons;

public static class Expansions
{
	#region WebApplication Expansions
	public static IApplicationBuilder UseAuthorizationR401(this WebApplication app) => app.UseMiddleware<Authorization401Middleware>();
	#endregion

	public static IEnumerable<string> FindAllValue(this ClaimsPrincipal user, string type) => user.FindAll(type).Select(it => it.Value);
}
