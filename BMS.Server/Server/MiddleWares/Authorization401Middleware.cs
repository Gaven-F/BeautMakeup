using Masuit.Tools.Mime;

namespace Server.MiddleWares;

public class Authorization401Middleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		var metadata = context.GetEndpoint()?.Metadata;
		// 未通过授权
		if (metadata == null ||
			!metadata.Any(x => x.GetType() == typeof(AllowAnyone)) &&
			(context.User?.Identity == null || !context.User.Identity.IsAuthenticated))
		{
			await R403(context);
		}
		// 授权通过，继续处理请求
		else await next(context);

		static async Task R403(HttpContext context)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.ContentType = MimeMapper.MimeTypes[".json"];
			await context.Response.WriteAsync(new { Msg = "授权失败！" }.ToJsonString());
		}
	}
}
