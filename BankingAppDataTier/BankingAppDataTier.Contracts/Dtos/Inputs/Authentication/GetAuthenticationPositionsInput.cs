using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class GetAuthenticationPositionsInput : OperationInput
    {
        public required string ClientId { get; set; }

        public int? NumberOfPositions { get; set; }

    }
}
