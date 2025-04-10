﻿using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class GetLoansOfClientOutput : OperationOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}
