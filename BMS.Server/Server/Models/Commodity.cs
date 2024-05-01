#nullable disable

namespace Server.Models;

public class Commodity : BasicEntityTable, IDbTable
{
	/// <summary>
	/// 商品名称
	/// </summary>
	public string Name { get; set; } = string.Empty;
	/// <summary>
	/// 商品价格
	/// </summary>
	public double Price { get; set; }
	/// <summary>
	/// 商品标签
	/// </summary>
	public List<Tag> Tags { get; set; }
	/// <summary>
	/// 商品图片信息
	/// </summary>
	public List<ImgInfo> ImgInfos { get; set; }
	/// <summary>
	/// 商品描述
	/// </summary>
	public string Description { get; set; }
}

public class Tag
{
	public string Name { get; set; } = string.Empty;

	public Tag()
	{

	}
}

public class ImgInfo
{
	public string ImgName { get; set; } = string.Empty;
	public string Type { get; set; }

	public ImgInfo()
	{

	}
}
