using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoanByIdOutput : BaseOutput
    {
        public LoanDto? Loan { get; set; }
    }
}
