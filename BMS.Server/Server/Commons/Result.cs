global using R = Server.Commons.Result;

namespace Server.Commons;

public class Result : IActionResult
{
	public string Code { get; set; } = RM.SUCCESS;

	public string Msg { get; set; } = RC.SUCCESS;

	public object? Data { get; set; }

	public object? Extra { get; set; }

	public DateTimeOffset Time { get; protected init; } = DateTimeOffset.Now;

	//TODO 考虑特殊情况的代码
	//private readonly string[] SpecialCode = [
	//	RC.FORBIDDEN,RC.UNAUTHORIZED
	//];

	public async Task ExecuteResultAsync(ActionContext context)
	{
		//if (SpecialCode.Contains(Code))
		//{
		//	return;
		//}

		var res = context.HttpContext.Response;
		res.StatusCode = StatusCodes.Status200OK;
		res.ContentType = "application/json";
		await res.WriteAsync(this.ToJsonString());
		return;
	}

	/// <summary>
	/// 通用成功返回标识
	/// </summary>
	public static R Success(object? data = null, object? extra = null) =>
		new() { Data = data, Extra = extra };

	/// <summary>
	/// 成功创建数据
	/// </summary>
	public static R Created(object? data = null, object? extra = null) =>
		new()
		{
			Code = RC.CREATED,
			Msg = RM.CREATED,
			Data = data,
			Extra = extra
		};

	/// <summary>
	/// 通用失败返回标识
	/// </summary>
	public static R Failure(object? data = null, object? extra = null) =>
		new()
		{
			Code = RC.FAILURE,
			Msg = RM.FAILURE,
			Data = data,
			Extra = extra
		};

	/// <summary>
	/// 未登录
	/// </summary>
	public static R Unauthorized() => new() { Code = RC.UNAUTHORIZED, Msg = RM.UNAUTHORIZED };

	/// <summary>
	/// 未授权
	/// </summary>
	public static R Forbidden() => new() { Code = RC.FORBIDDEN, Msg = RM.FORBIDDEN };

	public static R UidExist() => new() { Code = RC.UID_EXIST, Msg = RM.UID_EXIST };

	public static R ArgNull() => new() { Code = RC.ARG_NULL, Msg = RM.ARG_NULL };

	public static R UidUnExist() => new() { Code = RC.UID_ERR, Msg = RM.UID_ERR };
}
