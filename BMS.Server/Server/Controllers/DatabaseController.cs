using Microsoft.AspNetCore.Authorization;
using Server.Cores;
using Server.Interface;

namespace Server.Controllers;

[Authorize(Roles = "DatabaseAdmin")]
public class DatabaseController(DbService dbService) : ApiController
{
	public ISqlSugarClient Db { get => dbService.Instance; }

	public ActionResult Init()
	{
		Type[] tables;

		tables = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(assembly => assembly.GetTypes())
			.Where(t => t.GetInterfaces().Contains(typeof(IDbTable)))
			.ToArray();

		Db.DbMaintenance.CreateDatabase();
		Db.CodeFirst.InitTables(tables);
		return Ok();
	}


}
