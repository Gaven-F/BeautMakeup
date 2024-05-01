#nullable disable

using System.Text.Json.Serialization;

namespace Server.Models;

public class UserAndRole : IIsDelete, IDbTable
{
	//[SugarColumn(IsPrimaryKey = true)]
	public string UserId { get; set; } = string.Empty;

	//[SugarColumn(IsPrimaryKey = true)]
	public string RoleId { get; set; } = string.Empty;

	public bool IsDelete { get; set; } = false;
}

public class User : BasicEntityTable, IDbTable
{
	public string Name { get; set; } = string.Empty;

	public string Uid { get; set; } = string.Empty;

	public string Pwd { get; set; } = string.Empty;

	[Navigate(typeof(UserAndRole), nameof(UserAndRole.UserId), nameof(UserAndRole.RoleId))]
	public List<Role> Roles { get; set; }
}

public class Role : BasicEntityTable, IDbTable
{
	public string RoleVal { get; set; } = string.Empty;

	[JsonIgnore, NewJsonIgnore]
	[Navigate(typeof(UserAndRole), nameof(UserAndRole.RoleId), nameof(UserAndRole.UserId))]
	public List<User> Users { get; set; }
}
