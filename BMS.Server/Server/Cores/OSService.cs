using Masuit.Tools.Systems;
using OBS;
using OBS.Model;

namespace Server.Cores;

public class OSService
{
	private ObsClient Client { get; set; }

	private OSConfiguration OsConfi { get; set; }

	public OSService(IConfiguration confi)
	{
		OsConfi = new OSConfiguration();
		confi.Bind("OSConfig", OsConfi);
		var properties = OsConfi
			.GetType()
			.GetProperties(
				System.Reflection.BindingFlags.Default |
					System.Reflection.BindingFlags.Public |
					System.Reflection.BindingFlags.GetProperty |
					System.Reflection.BindingFlags.SetProperty);

		properties.ForEach(
			p =>
			{
				if (p.GetValue(OsConfi).IsNullOrEmpty())
				{
					var value = Environment.GetEnvironmentVariable(StringUtils.ToUpperSnakeCase(p.Name));
					if (value != null)
					{
						p.SetValue(OsConfi, value);
					}
					else
					{
						throw new ArgumentNullException($"未指定{p.Name}！");
					}
				}
			});

		Client = new ObsClient(OsConfi.AccessKey, OsConfi.SecretKey, OsConfi.Endpoint);
	}

	public void PutFileAsStream(Stream stream, string? fileName = null, string? fileType = null)
	{
		if (stream is FileStream file && fileName.IsNullOrEmpty())
		{
			fileName = file.Name;
		}
		if (fileName.IsNullOrEmpty())
		{
			fileName = SnowFlakeNew.NewId;
		}
		if (!fileType.IsNullOrEmpty())
		{
			fileName += $".{fileType}";
		}


		var request = new PutObjectRequest()
		{
			BucketName = OsConfi.BucketName,
			ObjectKey = $@"{OsConfi.SubNode}/{fileName}",
			InputStream = stream,
			CannedAcl = CannedAclEnum.PublicRead,
		};

		Client.PutObject(request);
	}

	public bool GetFileStream(string fileName, out Stream stream)
	{
		stream = new MemoryStream();
		ArgumentException.ThrowIfNullOrEmpty(fileName);

		var request = new GetObjectRequest()
		{
			BucketName = OsConfi.BucketName,
			ObjectKey = $@"{OsConfi.SubNode}/{fileName}",
		};

		using var response = Client.GetObject(request);
		response.OutputStream.CopyTo(stream);
		return response.StatusCode == System.Net.HttpStatusCode.OK;
	}

	private class OSConfiguration
	{
		public string AccessKey { get; set; } = string.Empty;

		public string SecretKey { get; set; } = string.Empty;

		public string Endpoint { get; set; } = string.Empty;

		public string BucketName { get; set; } = string.Empty;

		public string SubNode { get; set; } = "DEFAULT";
	}
}
