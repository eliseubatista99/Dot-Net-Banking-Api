using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class GetClientByIdOutput : BaseOutput
    {
        public required ClientDto? Client { get; set; }
    }
}
