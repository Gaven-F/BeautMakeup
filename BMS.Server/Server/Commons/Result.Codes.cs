global using RC = Server.Commons.Results.Codes;

namespace Server.Commons.Results;

public class Codes
{
	public const string SUCCESS = "R_20000";
	public const string CREATED = "R_20010";

	public const string FAILURE = "R_40000";
	public const string UNAUTHORIZED = "R_40001";
	public const string FORBIDDEN = "R_40003";
	public const string UID_EXIST = "R_40010";
	public const string UID_ERR = "R_40011";
	public const string ARG_NULL = "R_40020";
}
