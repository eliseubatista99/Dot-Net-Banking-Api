using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    public class DeleteClientInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public required string Id { get; set; }
    }
}
