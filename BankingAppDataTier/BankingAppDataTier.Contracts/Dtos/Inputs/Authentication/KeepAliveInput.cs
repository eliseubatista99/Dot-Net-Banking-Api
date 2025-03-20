using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class KeepAliveInput
    {
        public required string Token { get; set; }

    }
}
