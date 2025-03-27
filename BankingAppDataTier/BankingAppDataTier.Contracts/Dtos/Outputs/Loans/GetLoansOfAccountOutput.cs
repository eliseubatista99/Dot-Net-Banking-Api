using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class GetLoansOfAccountOutput : OperationOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}
