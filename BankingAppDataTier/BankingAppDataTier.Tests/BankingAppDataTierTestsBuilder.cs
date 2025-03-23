using ElideusDotNetFramework.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Tests
{
    public class BankingAppDataTierTestsBuilder : ElideusDotNetFrameworkTestsBuilder
    {
        protected override ElideusDotNetFrameworkMocksBuilder? MocksBuilder { get; set; } = new BankingAppDataTierMocksBuilder();
    }
}
