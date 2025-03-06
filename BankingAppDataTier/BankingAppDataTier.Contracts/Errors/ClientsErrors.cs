using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class ClientsErrors
    {
        public static BankingAppDataTierError CantCloseWithActiveAccounts = new BankingAppDataTierError { Code = "CantCloseWithActiveAccounts", Message = "In order to delete the client first close the related accounts" };
    }
}
