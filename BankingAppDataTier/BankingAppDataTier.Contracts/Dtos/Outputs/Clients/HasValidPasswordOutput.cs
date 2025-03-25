using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class HasValidPasswordOutput : OperationOutput
    {
        public required bool Result { get; set; }
    }
}
