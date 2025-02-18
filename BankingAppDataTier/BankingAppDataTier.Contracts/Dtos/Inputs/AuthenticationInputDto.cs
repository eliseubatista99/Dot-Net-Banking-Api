using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs
{
    public class AuthenticationInputDto
    {
        public required string AppId { get; set; }
    }
}
