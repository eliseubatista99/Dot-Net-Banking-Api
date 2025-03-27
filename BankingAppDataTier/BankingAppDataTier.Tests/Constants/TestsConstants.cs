using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Tests.Constants
{
    public static class TestsConstants
    {
        public const string ConnectionString = "Server=localhost;Port=5432;User Id=DotNetBankingApp;Password=DotNetBankingApp;Database=DotNetBankingAppTests;IncludeErrorDetail=true;";

        public const string AuthenticationTierUrl = "https://localhost:5003";

        public const string PermanentToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJlNTU3OTU2MC1lNDQ2LTQ5ODgtOTVlOS1hZTkyMzcyY2YyNzQiLCJuYW1lIjoiUGVybWFuZW50X0NsaWVudF8wMSIsIm5iZiI6MTc0MjU1MjExNiwiZXhwIjoxNzQyNTUzOTE2LCJpYXQiOjE3NDI1NTIxMTYsImlzcyI6ImJhbmtpbmdhcHB0ZXN0cyIsImF1ZCI6ImJhbmtpbmdhcHB0ZXN0cyJ9.di3BeV2twRCuDJZosLjer-LyQzp4vFLmQTAJiBXW98w";

        public const string ExpiredToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4NmJhZDBkNC00YjhmLTQ0MTgtYWI3Mi1hYmM2MDBhMGI1Y2UiLCJuYW1lIjoiUGVybWFuZW50X0NsaWVudF8wMSIsIm5iZiI6MTc0MjU1MTg3OSwiZXhwIjoxNzQyNTUzNjc5LCJpYXQiOjE3NDI1NTE4NzksImlzcyI6ImJhbmtpbmdhcHB0ZXN0cyIsImF1ZCI6ImJhbmtpbmdhcHB0ZXN0cyJ9.ElPFjh0YDOApo0X7nYIFx0RDNSSiGLRZhhRwLD33_CA";

        public static InputMetadata TestsMetadata = new InputMetadata
        {
            Token = PermanentToken,
        };
    }
}
