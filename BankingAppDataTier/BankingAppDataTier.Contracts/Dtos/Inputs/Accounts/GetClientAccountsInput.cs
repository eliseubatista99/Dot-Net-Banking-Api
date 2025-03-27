using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class GetClientAccountsInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string ClientId { get; set; }
    }
}
