using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class AuthenticateInput
    {
        public required string ClientId { get; set; }

        public required AuthenticationCodeDto AuthenticationCode { get; set; }

    }
}
