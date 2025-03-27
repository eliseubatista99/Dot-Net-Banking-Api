﻿using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetCardsOfAccountInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string AccountId { get; set; }
    }
}
