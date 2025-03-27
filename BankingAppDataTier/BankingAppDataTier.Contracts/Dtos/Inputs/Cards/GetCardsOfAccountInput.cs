using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class GetCardsOfAccountInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string AccountId { get; set; }
    }
}
