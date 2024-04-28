using Masuit.Tools.Mime;
using Microsoft.AspNetCore.Authorization;

namespace Server.MiddleWares;

public class Authorization401Middleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		var metadata = context.GetEndpoint()?.Metadata;
		// 未通过授权
		var user = context.User;

		if (metadata == null)
		{
			await R401(context);
			return;
		}

		if (metadata.Any(x => x.GetType() == typeof(AllowAnyone)))
		{
			await next(context);
			return;
		}

		var authorize = metadata.GetMetadata<AuthorizeAttribute>()!;
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
		return;


		static async Task R401(HttpContext context)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.ContentType = MimeMapper.MimeTypes[".json"];
			await context.Response.WriteAsync(new { Msg = "授权失败！" }.ToJsonString());
		}
	}
}
