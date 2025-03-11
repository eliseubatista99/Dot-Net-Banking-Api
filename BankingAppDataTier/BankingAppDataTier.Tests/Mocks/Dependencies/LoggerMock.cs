using Microsoft.Extensions.Logging;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class LoggerMock<T>
    {
        private static ILogger<T> _logger;

        public static ILogger<T> Mock()
        {
            if(_logger != null)
            {
                return _logger;
            }

            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());

            _logger = loggerFactory.CreateLogger<T>();

            return _logger;
        }
    }
}
