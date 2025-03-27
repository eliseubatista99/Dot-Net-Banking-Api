using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class GetAuthenticationPositionsOutput : OperationOutput
    {
        public required List<int> Positions { get; set; }
    }
}
