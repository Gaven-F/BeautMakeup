namespace Server.Cores;

public class DbService
{
	public ISqlSugarClient Instance { get; set; }

	public DbService(IConfiguration confServices)
	{
		var conf = new ConnectionConfig();

		confServices.Bind("DbConf", conf);

		//conf.ConfigureExternalServices = new()
		//{
		//	EntityNameService = (type, entity) =>
		//	{
		//		entity.DbTableName = StringUtils.ToSnakeCase(type.Name);
		//	},
		//};

		Instance = new SqlSugarClient(
			conf,
			(db) =>
			{
				db.Aop.OnLogExecuting = (sql, pars) =>
				{
					//获取原生SQL推荐 5.1.4.63  性能OK
					Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

					//获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
					//Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))
				};
			}
		);
	}
}
