﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoansOfClientOutput : OperationOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}
