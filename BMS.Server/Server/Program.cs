using System.Text.Json;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

//var modelBuilder = new ODataConventionModelBuilder();
//modelBuilder.EntitySet<Customer>("Customer");
//modelBuilder.EntitySet<TestModel>("Test");

builder
	.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
	})
	.AddOData(options =>
	{
		options.EnableQueryFeatures();
		//.AddRouteComponents("oData", modelBuilder.GetEdmModel());
	});

builder.Services.AddOpenApiDocument(confi =>
{
	confi.DocumentName = "default";

	confi.PostProcess = doc =>
	{
		doc.Info.Title = "BMS API";
	};
});

var app = builder.Build();

app.UseOpenApi().UseSwaggerUi().UseReDoc(config => config.Path = "/redoc");

app.UseAuthorization();
app.UseODataBatching().UseODataQueryRequest().UseODataRouteDebug();

app.MapControllers();

app.Run();
