using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class DeleteAccountInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }
    }
}
