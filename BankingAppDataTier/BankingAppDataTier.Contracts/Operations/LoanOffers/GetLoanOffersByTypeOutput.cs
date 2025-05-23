﻿using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class GetLoanOffersByTypeOutput : OperationOutput
    {
        public required List<LoanOfferDto> LoanOffers { get; set; }
    }
}
