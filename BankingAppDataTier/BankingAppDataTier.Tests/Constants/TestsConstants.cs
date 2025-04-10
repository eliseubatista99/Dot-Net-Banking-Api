using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Tests.Constants
{
    public static class TestsConstants
    {
        public const string ConnectionString = "Server=localhost;Port=5432;User Id=DotNetBankingApp;Password=DotNetBankingApp;Database=DotNetBankingAppTests;IncludeErrorDetail=true;";

        public const string AuthenticationTierUrl = "https://localhost:5003";

        public static InputMetadata TestsMetadata = new InputMetadata
        {
            Language = ElideusDotNetFramework.Core.Enums.Language.English, 
        };
    }
}
