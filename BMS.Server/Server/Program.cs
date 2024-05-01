using Mapster;
using Masuit.Tools.Systems;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

#region SqlSugar Global Config
StaticConfig.CodeFirst_MySqlCollate = "utf8mb4_bin";
#endregion

#region Mapster Global Config
TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
TypeAdapterConfig<UserDto, User>.ForType().Ignore(it => it.Roles);
#endregion

#region Masuit Global Config
SnowFlakeNew.SetMachienId(1);
#endregion

var builder = WebApplication.CreateBuilder(args);

#region DI
builder
	.Services.AddScoped<DbService>()
	.AddScoped(typeof(DbService.Repository<>))
	.AddSingleton<OSService>();
#endregion

#region Authentication
builder
	.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		builder.Configuration.Bind("CookieOptions", options);
		options.Events = new()
		{
			OnRedirectToAccessDenied = Expansions.OnRedirectToAccessDenied,
			OnRedirectToLogin = Expansions.OnRedirectToLogin,
		};
	});

builder.Services.AddControllers(options =>
{
	// Global api add authorization attribute
	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	options.Filters.Add(new AuthorizeFilter(policy));
});

#endregion

#region Cors
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials();
		policy.SetIsOriginAllowed(origin => true);
	});
});
#endregion

#region OpenApi
builder.Services.AddOpenApiDocument(confi =>
{
	confi.DocumentName = "default";
	confi.PostProcess = doc => doc.Info.Title = "BMS API";
});
#endregion

var app = builder.Build();

#region Config Middleware

app.UseOpenApi().UseSwaggerUi().UseReDoc(config => config.Path = "/redoc");

app.UseAuthentication().UseAuthorization().UseCookiePolicy().UseCors();

app.MapControllers();
#endregion

app.Run();
