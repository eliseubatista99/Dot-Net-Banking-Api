using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class LoanOffersErrors
    {
        public static BankingAppDataTierError CantDeleteWithRelatedLoans = new BankingAppDataTierError { Code = "CantDeleteWithRelatedLoans", Message = "In order to delete a Loan Offer first delete the related loans." };
    }
}
