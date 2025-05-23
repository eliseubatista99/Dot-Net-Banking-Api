﻿using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class GetPlasticsOfTypeOutput : OperationOutput
    {
        public required List<PlasticDto> Plastics { get; set; }
    }
}
