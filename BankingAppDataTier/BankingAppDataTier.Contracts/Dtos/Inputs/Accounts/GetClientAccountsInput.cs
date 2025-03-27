using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    [ExcludeFromCodeCoverage]

    public class GetClientAccountsInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string ClientId { get; set; }
    }
}
