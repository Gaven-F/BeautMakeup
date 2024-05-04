#nullable disable

using System.Text.Json.Serialization;

namespace Server.Models;

#region Mapping
public class CommodityAndTag : IIsDelete, IDbTable
{
	public string CommodityId { get; set; } = string.Empty;

	public string TagId { get; set; } = string.Empty;

	public bool IsDelete { get; set; }

	public CommodityAndTag() { }
}
#endregion

public class Commodity : BasicEntityTable, IDbTable
{
	/// <summary>
	/// 商品名称
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// 商品价格
	/// </summary>
	public double Price { get; set; }

	/// <summary>
	/// 商品标签
	/// </summary>
	[JsonIgnore, NewJsonIgnore]
	[Navigate(
		typeof(CommodityAndTag),
		nameof(CommodityAndTag.CommodityId),
		nameof(CommodityAndTag.TagId)
	)]
	public List<Tag> Tags { get; set; }

	/// <summary>
	/// 商品图片信息
	/// </summary>
	[JsonIgnore, NewJsonIgnore]
	[Navigate(NavigateType.OneToMany, nameof(ImgInfo.CommodityId))]
	public List<ImgInfo> ImgInfos { get; set; }

	/// <summary>
	/// 商品描述
	/// </summary>
	[SugarColumn(Length = 1024)]
	public string Descriptions { get; set; }
}

public class Tag : BasicEntityTable, IDbTable
{
	public string Name { get; set; }

	[JsonIgnore, NewJsonIgnore]
	[Navigate(
		typeof(CommodityAndTag),
		nameof(CommodityAndTag.TagId),
		nameof(CommodityAndTag.CommodityId)
	)]
	public Commodity Commodities { get; set; }

	public Tag() { }
}

public class ImgInfo : BasicEntityTable, IDbTable
{
	public string CommodityId { get; set; }

	public string ImgDescriptions { get; set; }

	/// <summary>
	/// 图片名称，在OS存储中的标识符
	/// </summary>
	public string ImgName { get; set; }

	public ImgType ImgType { get; set; } = ImgType.Default;

	public ImgInfo() { }
}

public enum ImgType
{
	/// <summary>
	/// 默认展示图片
	/// </summary>
	Default,
	/// <summary>
	/// 人物融合模板图片
	/// </summary>
	Template,
}
