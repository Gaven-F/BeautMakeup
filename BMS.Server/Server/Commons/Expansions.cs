namespace Server.Commons;

public static class Expansions
{
	#region WebApplication Expansions
	public static IApplicationBuilder UseAuthorizationR401(this WebApplication app) => app.UseMiddleware<Authorization401Middleware>();
	#endregion
}
