using Masuit.Tools.Systems;

namespace Server.Commons;

public class BasicEntityTable
{
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; } = SnowFlakeNew.NewId;

    [SugarColumn(ColumnDataType = "DateTime")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    [SugarColumn(ColumnDataType = "DateTime")]
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    public bool IsDelete { get; set; } = false;
}
