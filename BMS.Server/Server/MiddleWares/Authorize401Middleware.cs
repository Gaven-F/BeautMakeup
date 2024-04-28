using Masuit.Tools.Mime;
using Microsoft.AspNetCore.Authorization;

namespace Server.MiddleWares;

public class Authorize401Middleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		var metadata = context.GetEndpoint()?.Metadata;
		var user = context.User;

		// 无终结点或者是任意人员可以访问的终结点，直接放行
		if (metadata == null || metadata.Any(x => x.GetType() == typeof(AllowAnyone)))
		{
			await next(context);
			return;
		}

		var authorize = metadata.GetMetadata<AuthorizeAttribute>();

		// 如果是无需认证的终结点，直接放行
		if (authorize == null)
		{
			await next(context);
			return;
		}

		// 通过用户数据判断是否放行
		if (user != null)
		{
			if (authorize.Roles == null)
			{
				if (user.Identity != null && user.Identity.IsAuthenticated)
				{
					await next(context);
					return;
				}

				await R401(context);
				return;
			}

			if (authorize.Roles.Split(",").Intersect(user.FindAllValue(CT.Role)).Any())
			{
				await next(context);
				return;
			}
		}

		await R401(context);
	}

	private static async Task R401(HttpContext context)
	{
		context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		context.Response.ContentType = MimeMapper.MimeTypes[".json"];
		await context.Response.WriteAsync(new { Msg = "授权失败！" }.ToJsonString());
	}
}
