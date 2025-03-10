using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Providers;
using BankingAppDataTier.Tests.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
