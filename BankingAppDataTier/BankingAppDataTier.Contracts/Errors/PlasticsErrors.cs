using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Errors
{
    [ExcludeFromCodeCoverage]
    public static class PlasticsErrors
    {
        public static Error CantDeleteWithRelatedCards = new Error { Code = "CantDeleteWithRelatedCards", Message = "In order to delete a plastic first delete the related cards." };
    }
}
