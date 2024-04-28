global using RM = Server.Commons.Results.Msg;

namespace Server.Commons.Results;

public class Msg
{
	public const string SUCCESS = "SUCCESS";
	public const string CREATED = "数据创建成功";

	public const string FAILURE = "FAILURE";
	public const string UNAUTHORIZED = "未登录";
	public const string FORBIDDEN = "禁止操作";
	public const string UID_EXIST = "账号已存在";
	public const string UID_ERR = "账号或者密码错误";
	public const string ARG_NULL = "参数为空";
}
