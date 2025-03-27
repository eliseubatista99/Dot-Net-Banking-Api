using AutoMapper;
using BankingAppAuthenticationTier.Contracts.Constants.Database;
using BankingAppAuthenticationTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.MapperProfiles
{
    [ExcludeFromCodeCoverage]
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
             .ForMember(d => d.Id, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_ID)))
             .ForMember(d => d.Password, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_PASSWORD)));
        }
    }
}
