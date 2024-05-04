global using RC = Server.Commons.Results.Constants;

namespace Server.Commons.Results;

public class Constants
{
	public string Code { get; set; } = string.Empty;

	public string Msg { get; set; } = string.Empty;

	public static readonly RC SUCCESS = new() { Code = "R_20000", Msg = "SUCCESS" };

	public static readonly RC CREATED = new() { Code = "R_20010", Msg = "数据创建成功" };

	public static readonly RC FAILURE = new() { Code = "R_40000", Msg = "FAILURE" };

	public static readonly RC UNAUTHORIZED = new() { Code = "R_40001", Msg = "未登录" };

	public static readonly RC FORBIDDEN = new() { Code = "R_40003", Msg = "禁止操作" };

	public static readonly RC UID_EXIST = new() { Code = "R_40010", Msg = "账号已存在" };

	public static readonly RC UID_ERR = new() { Code = "R_40011", Msg = "账号或者密码错误" };

	public static readonly RC ARG_NULL = new() { Code = "R_40020", Msg = "参数为空" };
}
