using AutoMapper;
using BankingAppAuthenticationTier.Contracts.Constants.Database;
using BankingAppAuthenticationTier.Contracts.Database;
using BankingAppAuthenticationTier.Database;
using Npgsql;

namespace BankingAppAuthenticationTier.MapperProfiles
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ClientsMapperProfile : Profile
    {

        public ClientsMapperProfile()
        {
            this.CreateMapOfEntities();
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<NpgsqlDataReader, ClientsTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_ID)))
             .ForMember(d => d.Password, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_PASSWORD)));
        }
    }
}
