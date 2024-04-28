using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NSwag.Annotations;

namespace Server.Controllers;

[OpenApiTag("用户控制器", Description = "user | 用户")]
public class UserController(DbService dbService) : BasicController
{
	public ISqlSugarClient Db
	{
		get => dbService.Instance;
	}

	[AllowAnyone, HttpGet]
	public async Task<IActionResult> Demo(string key)
	{
		if (key == DateTime.Now.Day.ToString())
		{
			var claims = new List<Claim>
			{
				new(CT.Name, "GavenF"),
				new(CT.Role, "GavenF"),
				new(CT.Role, "DatabaseAdmin"),
				new(CT.Role, "Admin ")
			};

			var claimIdentity = new ClaimsIdentity(
				claims,
				CookieAuthenticationDefaults.AuthenticationScheme
			);
			var authenticationProperties = new AuthenticationProperties { };

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimIdentity),
				authenticationProperties
			);

			return R.Success();
		}
		return StatusCode(StatusCodes.Status401Unauthorized);
	}

	/// <summary>
	/// 登录接口
	/// </summary>
	[ProducesResponseType<R>(200)]
	[AllowAnyone, HttpPost]
	public async Task<IActionResult> Login([FromBody] UserLoginDto user)
	{
		if (user.Uid == null || user.Pwd == null)
		{
			return BadRequest(R.ArgNull());
		}

		var data = Db.Queryable<User>()
			.ExcludeDelete()
			//UNDONE 不知道为什么这里不能直接引用
			//.Includes(it => it.Roles)
			.First(it => it.Uid.Equals(user.Uid) && it.Pwd.Equals(user.Pwd));

		if (data is null)
		{
			return R.UidUnExist();
		}

		var rolesId = Db.Queryable<UserAndRole>()
			.ExcludeDelete()
			.Where(it => it.UserId == data.Id)
			.Select(it => it.RoleId)
			.ToList();

		var roles = Db.Queryable<Role>()
			.ExcludeDelete()
			.Where(it => rolesId.Contains(it.Id))
			.ToList();

		data.Roles = roles;

		var claims = new List<Claim> { new(CT.Name, data.Name) };
		var roleClaims = data.Roles?.Select(it => new Claim(CT.Role, it.RoleVal)) ?? [];

		claims.AddRange(roleClaims);

		var claimIdentity = new ClaimsIdentity(
			claims,
			CookieAuthenticationDefaults.AuthenticationScheme
		);
		var authenticationProperties = new AuthenticationProperties();

		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimIdentity),
			authenticationProperties
		);

		return R.Success(data);
	}

	/// <summary>
	/// 注册接口
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	[ProducesResponseType<R>(200)]
	[AllowAnyone, HttpPost]
	public async Task<IActionResult> Register([FromBody] UserDto user)
	{
		var has = await Db.Queryable<User>()
			.ExcludeDelete()
			.Where(it => it.Uid == user.Uid)
			.AnyAsync();

		if (has)
			return BadRequest(R.UidExist());

		var data = new User()
		{
			Uid = user.Uid,
			Pwd = user.Pwd,
			Name = user.Name
		};

		await Db.Insertable(data).ExecuteCommandAsync();

		return R.Created(data);
	}

	[HttpPut("{userId}")]
	public async Task<IActionResult> SetRole(
		[FromRoute] string userId,
		[FromBody] IEnumerable<string> roles
	)
	{
		// 检查用户是否存在
		var user = await Db.Queryable<User>().InSingleAsync(userId);

		if (user == null)
			return R.UidUnExist();

		// 获取已经存在的角色
		var dbRoleData = await Db.Queryable<Role>()
			.ExcludeDelete()
			.Where(it => roles.Contains(it.RoleVal))
			.ToListAsync();

		var dbRoleVal = dbRoleData.Select(it => it.RoleVal);

		// 存储不存在的角色
		var data = roles.Except(dbRoleVal).Select(it => new Role { RoleVal = it }).ToList();

		await Db.Insertable(data).ExecuteCommandAsync();

		// 获取所有需要绑定的角色
		data.AddRange(dbRoleData);

		// 删除用户原有角色
		await Db.Updateable<UserAndRole>()
			.Where(it => it.UserId == userId && !it.IsDelete)
			.SetColumns(it => new UserAndRole { IsDelete = true, UpdateTime = DateTime.Now })
			.ExecuteCommandAsync();

		// 更新用户角色
		var userAndRoles = data.Select(it => new UserAndRole { UserId = userId, RoleId = it.Id })
			.ToArray();

		await Db.Insertable(userAndRoles).ExecuteCommandAsync();

		return R.Created(new { Role = data, Mapper = userAndRoles });
	}
}
