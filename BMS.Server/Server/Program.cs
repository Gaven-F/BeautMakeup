using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Server.Controllers;
using Server.MiddleWares;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Demo>();
#region Authentication
builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	})
	.AddCookie(options => builder.Configuration.Bind("CookieOptions", options));

builder.Services.AddControllers(options =>
{
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

#region OData
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Customer>("Customer");
modelBuilder.EntitySet<TestModel>("Test");

builder.Services
	.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase)
	.AddOData(options =>
	{
		options.EnableQueryFeatures().AddRouteComponents("oData", modelBuilder.GetEdmModel());
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

app
	.UseODataQueryRequest()
	.UseODataBatching()
	.UseODataRouteDebug();

app.UseMiddleware<CustomAuthorizationMiddleware>();
app.MapControllers();
#endregion

app.Run();
