using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    [ExcludeFromCodeCoverage]

    public class GetClientByIdInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public required string Id { get; set; }
    }
}
