using System;
namespace Intermail.Services
{
	public interface IAppLogger
	{
		void Error(string message);
		void Info(string message);
	}
}

