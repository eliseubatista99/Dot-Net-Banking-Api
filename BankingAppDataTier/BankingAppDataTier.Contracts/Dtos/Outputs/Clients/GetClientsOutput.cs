using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Outputs
{
    public class GetClientsOutput : BaseOutput
    {
        public required List<ClientDto> Clients { get; set; }
    }
}
