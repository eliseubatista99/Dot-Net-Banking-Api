﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class GetClientsOutput : OperationOutput
    {
        public required List<ClientDto> Clients { get; set; }
    }
}
