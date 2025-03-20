using AutoMapper;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Database;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
             .ForMember(d => d.ClientId, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TokensTable.COLUMN_CLIENT_ID)))
             .ForMember(d => d.Token, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TokensTable.COLUMN_TOKEN)))
             .ForMember(d => d.ExpirationDate, opt => opt.MapFrom(s => DateTime.Parse(SqlDatabaseHelper.ReadColumnValue(s, TokensTable.COLUMN_EXPIRATION_DATE)!)));
        }
    }
}
