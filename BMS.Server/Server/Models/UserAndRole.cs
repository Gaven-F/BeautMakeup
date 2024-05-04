#nullable disable

using System.Text.Json.Serialization;

namespace Server.Models;

#region Mapping
public class UserAndRole : IIsDelete, IDbTable
{
	public string UserId { get; set; } = string.Empty;

	public string RoleId { get; set; } = string.Empty;

	public bool IsDelete { get; set; } = false;
}
#endregion

public class User : BasicEntityTable, IDbTable
{
	public string Name { get; set; }

	public string Uid { get; set; }

	public string Pwd { get; set; }

	[Navigate(typeof(UserAndRole), nameof(UserAndRole.UserId), nameof(UserAndRole.RoleId))]
	public List<Role> Roles { get; set; }

	[JsonIgnore, NewJsonIgnore]
	[Navigate(NavigateType.OneToMany, nameof(ShoppingCommodity.UserId))]
	public List<ShoppingCommodity> ShoppingCart { get; set; }
}

public class Role : BasicEntityTable, IDbTable
{
	public string RoleVal { get; set; }

	public Role(string role)
	{
		RoleVal = role;
	}

	public Role() { }

	[JsonIgnore, NewJsonIgnore]
	[Navigate(typeof(UserAndRole), nameof(UserAndRole.RoleId), nameof(UserAndRole.UserId))]
	public List<User> Users { get; set; }
}
