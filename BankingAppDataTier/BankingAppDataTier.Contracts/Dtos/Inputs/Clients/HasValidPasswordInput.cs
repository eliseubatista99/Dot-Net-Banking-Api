using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    [ExcludeFromCodeCoverage]

    public class HasValidPasswordInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the client password.
        /// </summary>
        /// <value>The client password.</value>
        public required string PassWord { get; set; }
    }
}
