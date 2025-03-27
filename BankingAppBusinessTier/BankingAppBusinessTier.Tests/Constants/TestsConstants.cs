using ElideusDotNetFramework.Operations;

namespace BankingAppBusinessTier.Tests.Constants
{
    public static class TestsConstants
    {
        private const string PermanentToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJlNTU3OTU2MC1lNDQ2LTQ5ODgtOTVlOS1hZTkyMzcyY2YyNzQiLCJuYW1lIjoiUGVybWFuZW50X0NsaWVudF8wMSIsIm5iZiI6MTc0MjU1MjExNiwiZXhwIjoxNzQyNTUzOTE2LCJpYXQiOjE3NDI1NTIxMTYsImlzcyI6ImJhbmtpbmdhcHB0ZXN0cyIsImF1ZCI6ImJhbmtpbmdhcHB0ZXN0cyJ9.di3BeV2twRCuDJZosLjer-LyQzp4vFLmQTAJiBXW98w";

        public const string DataTierUrl = "https://localhost:5002/";

        public static InputMetadata TestsMetadata = new InputMetadata
        {
            Token = PermanentToken,
        };
    }
}
