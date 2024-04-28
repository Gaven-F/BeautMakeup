using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult> Demo(string key)
    {
        if (key == DateTime.Now.Day.ToString())
        {
            var claims = new List<Claim>
            {
                new(CT.Name, "GavenF"),
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

            return Ok(User.FindAllValue(CT.Role));
        }
        return StatusCode(StatusCodes.Status401Unauthorized);
    }

    [AllowAnyone, HttpPost]
    public async Task<ActionResult> Login([FromBody] UserLoginDto user)
    {
        if (user.Uid == null || user.Pwd == null)
        {
            return BadRequest(CONST_DATA.ARG_NULL);
        }

        var data = Db.Queryable<User>()
            .ExcludeDelete()
            .Includes(it => it.Roles)
            .Single(it => it.Uid == user.Uid && it.Pwd == user.Pwd);

        if (data is null)
        {
            return BadRequest();
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

        return new OkResult();
    }

    [Authorize(Roles = "GavenF")]
    public ActionResult AdminApi() => new OkResult();
}
