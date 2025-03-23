using ElideusDotNetFramework.Errors.Contracts;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class PlasticsErrors
    {
        public static Error CantDeleteWithRelatedCards = new Error { Code = "CantDeleteWithRelatedCards", Message = "In order to delete a plastic first delete the related cards." };
    }
}
