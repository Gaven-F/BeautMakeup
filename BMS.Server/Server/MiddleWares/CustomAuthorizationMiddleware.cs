using Masuit.Tools;
using Masuit.Tools.Mime;
using Microsoft.AspNetCore.Authorization;

namespace Server.MiddleWares;

public class CustomAuthorizationMiddleware(RequestDelegate next)
{

	public async Task InvokeAsync(HttpContext context)
	{
		var matedata = context.GetEndpoint()?.Metadata;
		if (matedata == null)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			context.Response.ContentType = MimeMapper.MimeTypes[".json"];
			await context.Response.WriteAsync(new { Msg = "授权失败！" }.ToJsonString());
			return;
		}
		matedata.GetMetadata<AllowAnonymousAttribute>();

		// 授权通过，继续处理请求
		await next(context);
	}
}
