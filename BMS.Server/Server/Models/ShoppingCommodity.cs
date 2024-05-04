#nullable disable

using System.Text.Json.Serialization;

namespace Server.Models;

public class ShoppingCommodity : BasicEntityTable, IDbTable
{
	public string CommodityId { get; set; }

	public string UserId { get; set; }

	public int Count { get; set; }

	public bool WaitPay { get; set; } = true;

	public DateTime AddedTime { get; set; } = DateTime.Now;

	public DateTime PayTime { get; set; } = DateTime.Now;

	[JsonIgnore, NewJsonIgnore]
	[Navigate(NavigateType.OneToOne, nameof(CommodityId))]
	public Commodity Commodity { get; set; }

	[JsonIgnore, NewJsonIgnore]
	[Navigate(NavigateType.OneToOne, nameof(UserId))]
	public User User { get; set; }
}
