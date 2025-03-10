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
    public static class AuthMock
    {
        private static IAuthenticationProvider _auth;

        public static IAuthenticationProvider Mock(Dictionary<string, string?> mock)
        {
            var configuration = ConfigurationMock.Mock();

            _auth = new AuthenticationProvider(configuration);

            return _auth;
        }
    }
}
