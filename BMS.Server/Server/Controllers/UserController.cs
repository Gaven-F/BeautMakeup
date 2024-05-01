using System.Security.Claims;
using Mapster;
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
			.Includes(it => it.Roles.Where(r => !r.IsDelete).ToList())
			.First(it => it.Uid.Equals(user.Uid) && it.Pwd.Equals(user.Pwd));

		if (data is null)
		{
			return R.UidErr();
		}

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
		var u = await Db.Queryable<User>().FirstAsync(it => it.Uid == user.Uid);
		if (u != null)
		{
			return R.UidExist();
		}
		var data = user.Adapt<User>();
		data.Roles = [new("User")];
		data = await Db.InsertNav(data).Include(it => it.Roles).ExecuteReturnEntityAsync();
		return R.Created(data);
	}

	[HttpPut("{userId}")]
	public async Task<IActionResult> SetRole(
		[FromRoute] string userId,
		[FromBody] IEnumerable<string> roles
	)
	{
		// 检查用户是否存在
		var user = await Db.Queryable<User>().Includes(it => it.Roles).InSingleAsync(userId);

		if (user == null)
		{
			return R.UidErr();
		}

		var srcRoles = Db.Queryable<Role>()
			.GroupBy(it => it.RoleVal)
			.Where(it => roles.Contains(it.RoleVal))
			.Select(it => new { Id = SqlFunc.AggregateMax(it.Id) })
			.InnerJoin<Role>((g, r) => g.Id == r.Id)
			.Select((g, r) => r)
			.ToList();

		// 添加缺失的角色
		srcRoles
			.Where(r => !user.Roles.Any(it => it.RoleVal == r.RoleVal))
			.ToList()
			.ForEach(r => user.Roles.Add(r));

		// 移除多余的角色
		user.Roles.Where(r => !srcRoles.Any(it => it.RoleVal == r.RoleVal))
			.ToList()
			.ForEach(r => user.Roles.Remove(r));

		user.UpdateTime = DateTime.Now;

		await Db.UpdateNav(user)
			.Include(
				it => it.Roles,
				new UpdateNavOptions
				{
					ManyToManyEnableLogicDelete = true,
					ManyToManyIsUpdateA = true,
					ManyToManyIsUpdateB = true,
				}
			)
			.ExecuteCommandAsync();

		return R.Created(user);
	}
}
