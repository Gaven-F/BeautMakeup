using Mapster;
using Masuit.Tools.Systems;

namespace Server.Commons;

public class Global
{
	public static void GlobalStaticSetting()
	{
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
	}
}
