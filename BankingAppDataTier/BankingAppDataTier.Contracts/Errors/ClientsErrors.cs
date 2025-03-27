using ElideusDotNetFramework.Errors;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class ClientsErrors
    {
        public static Error CantCloseWithActiveAccounts = new Error { Code = "CantCloseWithActiveAccounts", Message = "In order to delete the client first close the related accounts" };
    }
}
