﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class AddCardInput : OperationInput
    {
        public required CardDto Card { get; set; }
    }
}
