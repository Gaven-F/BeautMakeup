using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Server.Commons;

public static partial class StringUtils
{
	/// <summary>
	/// 转小驼峰
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static string ToCamelCase(string str)
	{
		// 将字符串分割成单词
		string[] words = str.Split('_', StringSplitOptions.RemoveEmptyEntries);

		// 将第一个单词的首字母小写，其他单词的首字母大写
		var result = new StringBuilder(words[0]);
		for (int i = 1; i < words.Length; i++)
		{
			result.Append(char.ToUpper(words[i][0]));
			result.Append(words[i].AsSpan(1));
		}

		return result.ToString();
	}

	/// <summary>
	/// 转大驼峰
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static string ToPascalCase(string str)
	{
		// 将字符串分割成单词
		string[] words = str.Split('_', StringSplitOptions.RemoveEmptyEntries);

		// 将每个单词的首字母大写，并删除分隔符
		var textInfo = CultureInfo.CurrentCulture.TextInfo;
		for (int i = 0; i < words.Length; i++)
		{
			words[i] = textInfo.ToTitleCase(words[i]);
		}

		// 将单词连接起来
		return string.Concat(words);
	}

	/// <summary>
	/// 驼峰转下划线小写
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static string ToSnakeCase(string str)
	{
		var result = new StringBuilder();
		for (int i = 0; i < str.Length; i++)
		{
			if (char.IsUpper(str[i]))
			{
				// 如果是大写字母，则在其前面添加下划线，并将其转换为小写字母
				if (i > 0)
				{
					result.Append('_');
				}
				result.Append(char.ToLower(str[i]));
			}
			else
			{
				result.Append(str[i]);
			}
		}
		return result.ToString();
	}

	/// <summary>
	/// 驼峰转下划线大写
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static string ToUpperSnakeCase(string str)
	{
		// 使用正则表达式在驼峰形式的字符串中插入下划线，并在每个下划线前插入转义符号
		string snakeCase = UpperCaseSnakeCaseRegex().Replace(str, "_$1");

		// 将字符串转换为大写
		return snakeCase.ToUpper();
	}

	/// <summary>
	/// 非开头大写字母寻找正则表达式
	/// </summary>
	/// <returns></returns>
	[GeneratedRegex(@"(?<!^)([A-Z])")]
	private static partial Regex UpperCaseSnakeCaseRegex();
}
