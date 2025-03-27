using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Inputs.Transactions
{
    [ExcludeFromCodeCoverage]

    public class EditTransactionInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the Destination Name.
        /// </summary>
        public required string DestinationName { get; set; }
    }
}
