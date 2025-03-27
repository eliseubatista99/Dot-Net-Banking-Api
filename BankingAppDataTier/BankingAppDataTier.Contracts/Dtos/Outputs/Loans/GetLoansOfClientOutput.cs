﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoansOfClientOutput : OperationOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}
