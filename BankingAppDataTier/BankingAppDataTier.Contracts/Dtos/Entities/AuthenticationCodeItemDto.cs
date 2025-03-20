using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class AuthenticationCodeItemDto
    {
        public required int Position { get; set; }

        public required char Value { get; set; }
    }
}
