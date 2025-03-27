using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class GetCardByIdInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }
    }
}
