using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Providers;
using BankingAppDataTier.Tests.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseClientsProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseClientsProvider _dbProvider;

        public static IDatabaseClientsProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider == null)
                {


                    var _configuration = ConfigurationMock.Mock();
                    _dbProvider = new DatabaseClientsProvider(_configuration);

                    ClientsDatabaseMock.DefaultMock(_dbProvider, clearDatabase: true);
                }


                return _dbProvider;
            }
        }
    }
}
