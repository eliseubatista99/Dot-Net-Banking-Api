using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class DeleteCardInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }
    }
}
