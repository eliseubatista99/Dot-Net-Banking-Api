﻿using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class IsValidTokenOutput : OperationOutput
    {
        public required bool IsValid { get; set; }

        public string? Reason { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}
