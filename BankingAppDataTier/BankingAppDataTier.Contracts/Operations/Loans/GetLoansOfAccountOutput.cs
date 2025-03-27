using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Loans
{
    [ExcludeFromCodeCoverage]

    public class GetLoansOfAccountOutput : OperationOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}
