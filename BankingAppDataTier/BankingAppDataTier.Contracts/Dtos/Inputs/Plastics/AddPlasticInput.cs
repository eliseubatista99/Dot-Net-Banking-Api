﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    public class AddPlasticInput : OperationInput
    {
        public required PlasticDto Plastic { get; set; }
    }
}
