using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class AddLoanInput : OperationInput
    {
        public required LoanDto Loan { get; set; }
    }
}
