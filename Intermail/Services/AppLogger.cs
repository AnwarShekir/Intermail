using System;
namespace Intermail.Services
{
	public class AppLogger : IAppLogger
	{
        private readonly string _correlationId;
        private readonly ILogger _logger;

		public AppLogger(ILogger logger)
		{
            _logger = logger;
            _correlationId = Guid.NewGuid().ToString();
		}

        public void Error(string message)
        {
            _logger.LogError("ThreadId: {_correlationId} Message: {message}",_correlationId, message);
        }

        public void Info(string message)
        {
            _logger.LogInformation("ThreadId: {_correlationId} Message: {message}", _correlationId, message);
        }


    }
}

