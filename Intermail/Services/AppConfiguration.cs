using System;
namespace Intermail.Services
{
	public class AppConfiguration : IAppConfiguration
	{
		private readonly IConfiguration _config;

		public AppConfiguration(IConfiguration config)
		{
			_config = config;
		}

		public string AppToken => _config["AppToken"];

		public string ExternalToken => _config["ExternalToken"];

		public string ExternalUrl => _config["ExternalUrl"];

		public string ApiTokenHeaderName => _config["ApiTokenHeaderName"];
    }
}

