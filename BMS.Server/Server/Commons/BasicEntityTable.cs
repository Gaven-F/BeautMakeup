using System.Text.Json.Serialization;
using Masuit.Tools.Systems;

namespace Server.Commons;

public class BasicEntityTable : IIsDelete
{
	[SugarColumn(IsPrimaryKey = true)]
	public string Id { get; set; } = SnowFlakeNew.NewId;

	[SugarColumn(ColumnDataType = "DateTime")]
	[JsonIgnore, NewJsonIgnore]
	public DateTime CreateTime { get; set; } = DateTime.Now;

	[SugarColumn(ColumnDataType = "DateTime")]
	[JsonIgnore, NewJsonIgnore]
	public DateTime UpdateTime { get; set; } = DateTime.Now;

	[JsonIgnore, NewJsonIgnore]
	public bool IsDelete { get; set; } = false;
}

public interface IIsDelete
{
	[JsonIgnore, NewJsonIgnore]
	public bool IsDelete { get; set; }
}
