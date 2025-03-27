using AutoMapper;
using BankingAppAuthenticationTier.Contracts.Constants.Database;
using BankingAppAuthenticationTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class TokensMapperProfile : Profile
    {

        public TokensMapperProfile()
        {
            this.CreateMapOfEntities();
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<NpgsqlDataReader, TokenTableEntry>()
             .ForMember(d => d.ClientId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, TokensTable.COLUMN_CLIENT_ID)))
             .ForMember(d => d.Token, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, TokensTable.COLUMN_TOKEN)))
             .ForMember(d => d.ExpirationDate, opt => opt.MapFrom(s => DateTime.Parse(NpgsqlDatabaseHelper.ReadColumnValue(s, TokensTable.COLUMN_EXPIRATION_DATE)!)));
        }
    }
}
