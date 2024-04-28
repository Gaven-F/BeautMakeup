namespace Server.Commons;

public static class Expansions
{
	#region WebApplication Expansions
	public static IApplicationBuilder UseAuthorizationR403(this WebApplication app) => app.UseMiddleware<Authorization403Middleware>();
	#endregion
}
