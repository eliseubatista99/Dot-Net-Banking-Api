using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoansOfAccountOutput : OperationOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}
