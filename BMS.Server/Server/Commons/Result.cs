global using R = Server.Commons.Result;

namespace Server.Commons;

public class Result : IActionResult
{
	public string Code { get; set; } = RC.SUCCESS.Msg;

	public string Msg { get; set; } = RC.SUCCESS.Code;

	public object? Data { get; set; }

	public object? Extra { get; set; }

	public DateTimeOffset Time { get; protected init; } = DateTimeOffset.Now;

	public Result() { }

	public Result(RC rc)
	{
		Msg = rc.Msg;
		Code = rc.Code;
	}

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
		new(RC.CREATED) { Data = data, Extra = extra };

	/// <summary>
	/// 通用失败返回标识
	/// </summary>
	public static R Failure(object? data = null, object? extra = null) =>
		new(RC.FAILURE) { Data = data, Extra = extra };

	/// <summary>
	/// 未登录
	/// </summary>
	public static R Unauthorized() => new(RC.UNAUTHORIZED);

	/// <summary>
	/// 未授权
	/// </summary>
	public static R Forbidden() => new(RC.FORBIDDEN);

	public static R UidExist() => new(RC.UID_EXIST);

	public static R ArgNull() => new(RC.ARG_NULL);

	public static R UidErr() => new(RC.UID_ERR);
}
