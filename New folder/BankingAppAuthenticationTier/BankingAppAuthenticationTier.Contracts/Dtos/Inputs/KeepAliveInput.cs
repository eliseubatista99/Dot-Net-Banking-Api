using BankingAppAuthenticationTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppAuthenticationTier.Contracts.Dtos.Inputs
{
    public class KeepAliveInput
    {
        public required string Token { get; set; }

    }
}
