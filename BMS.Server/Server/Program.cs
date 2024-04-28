using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

#region Authentication
builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options => builder.Configuration.Bind("CookieOptions", options));

builder.Services.AddControllers(options =>
{
	// Global api add authorization attribute
	var policy = new AuthorizationPolicyBuilder()
					 .RequireAuthenticatedUser()
					 .Build();
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
builder.Services
	.AddOpenApiDocument(confi =>
	{
		confi.DocumentName = "default";
		confi.PostProcess = doc => doc.Info.Title = "BMS API";
	});
#endregion

var app = builder.Build();

#region Config Middleware

app
	.UseOpenApi()
	.UseSwaggerUi()
	.UseReDoc(config => config.Path = "/redoc");

app
	.UseAuthentication()
	.UseAuthorization()
	.UseCookiePolicy()
	.UseCors();

app.UseAuthorizeR401();

app.MapControllers();
#endregion

app.Run();
