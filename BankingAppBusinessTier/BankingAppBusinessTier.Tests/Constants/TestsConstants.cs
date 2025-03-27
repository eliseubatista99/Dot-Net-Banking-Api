using ElideusDotNetFramework.Core.Operations;

namespace BankingAppBusinessTier.Tests.Constants
{
    public static class TestsConstants
    {
        public const string AuthenticationTierUrl = "https://localhost:5003";

        public const string DataTierUrl = "https://localhost:5002";

        public static InputMetadata TestsMetadata = new InputMetadata
        {
            Token = "token",
        };
    }
}
