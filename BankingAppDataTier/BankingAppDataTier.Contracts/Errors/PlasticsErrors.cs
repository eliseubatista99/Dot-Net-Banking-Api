using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class PlasticsErrors
    {
        public static BankingAppDataTierError CantDeleteWithRelatedCards = new BankingAppDataTierError { Code = "CantDeleteWithRelatedCards", Message = "In order to delete a plastic first delete the related cards." };
    }
}
