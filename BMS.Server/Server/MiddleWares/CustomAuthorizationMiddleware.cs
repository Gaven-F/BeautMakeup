using Masuit.Tools.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Server.MiddleWares;

public class CustomAuthorizationMiddleware(RequestDelegate next)
{

	public async Task InvokeAsync(HttpContext context)
	{
		// 进行自定义的授权逻辑
		if (context.User.Identities == null || !context.User.Identity!.IsAuthenticated)
		{
			// 授权失败时返回自定义的响应
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			context.Response.ContentType = MimeMapper.MimeTypes[".json"];
			await context.Response.WriteAsync("{\"message\": \"授权失败\"}");
			return;
		}

		// 授权通过，继续处理请求
		await next(context);
	}
}

public class Demo : IAuthorizationMiddlewareResultHandler
{

	public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
	{
		Console.WriteLine("ENTER");
		if (authorizeResult.Forbidden)
		{
			Console.WriteLine("FORBIDDEN");
		}
		return Task.CompletedTask;
	}
}
