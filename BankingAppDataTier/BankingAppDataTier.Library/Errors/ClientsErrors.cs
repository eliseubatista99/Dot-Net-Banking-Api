using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Library.Errors
{
    [ExcludeFromCodeCoverage]
    public static class ClientsErrors
    {
        public static Error CantCloseWithActiveAccounts = new Error { Code = "CantCloseWithActiveAccounts", Message = "In order to delete the client first close the related accounts" };
    }
}
