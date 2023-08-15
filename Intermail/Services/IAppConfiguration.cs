using System;
namespace Intermail.Services
{
	public interface IAppConfiguration
	{

		public string AppToken { get; }
		public string ExternalToken { get; }
		public string ExternalUrl { get; }
		public string ApiTokenHeaderName { get;  }

	}
}

