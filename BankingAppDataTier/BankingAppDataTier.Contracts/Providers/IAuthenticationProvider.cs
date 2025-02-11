using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IAuthenticationProvider
    {
        public void AddAuthenticationToApplicationBuilder(ref WebApplicationBuilder builder);

        public void AddAuthorizationToSwaggerGen(ref WebApplicationBuilder builder);

        public string GenerateToken(string audience);
    }
}
