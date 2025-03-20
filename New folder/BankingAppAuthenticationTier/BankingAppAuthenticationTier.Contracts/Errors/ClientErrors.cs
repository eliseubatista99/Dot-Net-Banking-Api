using BankingAppAuthenticationTier.Contracts.Dtos.Entities;

namespace BankingAppAuthenticationTier.Contracts.Errors
{
    public static class ClientErrors
    {
        public static BankingAppAuthenticationTierError InvalidClient = new BankingAppAuthenticationTierError { Code = "InvalidClient", Message = "Client not found" };
    }
}
