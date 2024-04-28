namespace Server.Cores;

public class DbService
{
	public ISqlSugarClient Instance { get; set; }

	public DbService(IConfiguration confServices)
	{
		var conf = new ConnectionConfig();

		confServices.Bind("DbConf", conf);

		conf.ConfigureExternalServices = new()
		{
			EntityNameService = (type, entity) =>
			{
				entity.DbTableName = StringUtils.ToSnakeCase(type.Name);
			},

		};


		Instance = new SqlSugarClient(conf, (db) =>
		{
		});
	}
}
