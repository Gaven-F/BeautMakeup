using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;
using Server.Interface;

namespace Server.Controllers;

[Authorize(Roles = "DatabaseAdmin")]
[OpenApiTag("数据库控制器", Description = "db | database | 数据库")]
public class DatabaseController(DbService dbService) : BasicController
{
	public ISqlSugarClient Db
	{
		get => dbService.Instance;
	}

	/// <summary>
	/// 初始化数据库
	/// </summary>
	/// <returns></returns>
	[ProducesResponseType(typeof(R), 200)]
	public IActionResult Init(bool dropSourceData = false)
	{
		Type[] tables;

		tables = AppDomain
			.CurrentDomain.GetAssemblies()
			.SelectMany(assembly => assembly.GetTypes())
			.Where(t => t.GetInterfaces().Contains(typeof(IDbTable)))
			.ToArray();

		Db.DbMaintenance.CreateDatabase("bms");

		if (dropSourceData)
		{
			var dropTableNames = Db
				.DbMaintenance.GetTableInfoList(false)
				.Select(table => table.Name)
				.ToArray();
			Db.DbMaintenance.DropTable(dropTableNames);
		}

		Db.CodeFirst.InitTables(tables);
		return R.Success();
	}
}
