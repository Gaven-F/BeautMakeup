using Server.Interface;

namespace Server.Models;

public class UserAndRole : BasicEntityTable, IDbTable
{
    public string UserId { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
}

public class User : BasicEntityTable, IDbTable
{
    public string Name { get; set; } = string.Empty;
    public string Uid { get; set; } = string.Empty;
    public string Pwd { get; set; } = string.Empty;

    [Navigate(NavigateType.ManyToMany, nameof(UserAndRole.UserId), nameof(UserAndRole.RoleId))]
    public List<Role>? Roles { get; set; }
}

public class Role : BasicEntityTable, IDbTable
{
    public string RoleVal { get; set; } = string.Empty;

    [Navigate(NavigateType.ManyToMany, nameof(UserAndRole.RoleId), nameof(UserAndRole.UserId))]
    public List<User>? Users { get; set; }
}
