using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    public class AddClientInput
    {
        public required ClientDto Client { get; set; }

        public required string PassWord { get; set; }
    }
}
