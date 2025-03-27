using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class AddLoanInput : OperationInput
    {
        public required LoanDto Loan { get; set; }
    }
}
