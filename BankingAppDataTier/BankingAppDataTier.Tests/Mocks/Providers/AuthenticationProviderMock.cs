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
using System.Threading;
using System.Threading.Tasks;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class AuthenticationProviderMock
    {
        private static readonly object _lock = new object();

        private static AuthenticationProvider _authProvider;

        public static IAuthenticationProvider Mock()
        {
            lock (_lock)
            {
                if (_authProvider != null)
                {
                    return _authProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _authProvider = new AuthenticationProvider(_configuration);

                return _authProvider;
            }
        }
    }
}
