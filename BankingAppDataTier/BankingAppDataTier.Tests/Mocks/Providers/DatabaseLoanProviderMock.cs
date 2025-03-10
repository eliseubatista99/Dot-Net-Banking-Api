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
    public static class DatabaseLoanProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseLoanProvider _dbProvider;

        public static IDatabaseLoansProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _dbProvider = new DatabaseLoanProvider(_configuration);

                LoansDatabaseMock.DefaultMock(_dbProvider, clearDatabase: true);
                return _dbProvider;
            }
        }
    }
}
