using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Providers.Contracts;
using ElideusDotNetFramework.Tests.Mocks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankingAppDataTier.Tests.Mocks
{
    public class BakingAppDataTierApplicationContextMock : TestsApplicationContext
    {
        protected override TestsConfigurationBuilderMock TestsConfigurationBuilder { get; set; } = new BankingAppConfigurationBuilderMock();
    }
}
