namespace Server.Cores;

public class DbService
{
	public ISqlSugarClient Instance { get; set; }

	public DbService(IConfiguration confServices)
	{
		var conf = new ConnectionConfig();

		confServices.Bind("DbConf", conf);

		Instance = new SqlSugarClient(conf, (db) =>
		{
		});
	}
}
