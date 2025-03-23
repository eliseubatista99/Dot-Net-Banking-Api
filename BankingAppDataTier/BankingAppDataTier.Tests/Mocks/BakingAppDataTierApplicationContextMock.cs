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
    public class BakingAppDataTierApplicationContextMock : ApplicationContextMock
    {
        protected override Dictionary<string, string?> Configurations { get; set; } = new Dictionary<string, string?>
        {
            {$"{DatabaseConfigs.DatabaseSection}:{DatabaseConfigs.DatabaseConnection}", TestsConstants.ConnectionString},
            {$"{AuthenticationConfigs.AuthenticationSection}:{AuthenticationConfigs.Issuer}", TestsConstants.AuthenticationIssuer},
            {$"{AuthenticationConfigs.AuthenticationSection}:{AuthenticationConfigs.Audience}", TestsConstants.AuthenticationAudience},
            {$"{AuthenticationConfigs.AuthenticationSection}:{AuthenticationConfigs.Key}", TestsConstants.AuthenticationKey},
        };
    }
}
