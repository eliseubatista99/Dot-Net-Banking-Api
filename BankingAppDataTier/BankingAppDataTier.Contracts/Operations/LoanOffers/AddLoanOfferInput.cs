﻿using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class AddLoanOfferInput : OperationInput
    {
        public required LoanOfferDto LoanOffer { get; set; }
    }
}
