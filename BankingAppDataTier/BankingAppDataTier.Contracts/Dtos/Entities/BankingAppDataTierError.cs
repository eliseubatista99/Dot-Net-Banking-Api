using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Entities
{
    public class BankingAppDataTierError
    {
        public required string Code { get; set; }

        public required string Message { get; set; }
    }
}
