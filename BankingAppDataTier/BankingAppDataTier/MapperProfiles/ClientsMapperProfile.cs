﻿using AutoMapper;
using BankingAppDataTier.Library.Constants.Database;
using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.PostgreSql;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.MapperProfiles
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
            this.CreateMap<ClientsTableEntry, ClientDto>();

            this.CreateMap<ClientDto, ClientsTableEntry>()
             .ForMember(d => d.Password, opt => opt.MapFrom(s => string.Empty));

            this.CreateMap<NpgsqlDataReader, ClientsTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_ID)))
             .ForMember(d => d.Password, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_PASSWORD)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_NAME)))
             .ForMember(d => d.Surname, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_SURNAME)))
             .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_BIRTH_DATE)))
             .ForMember(d => d.VATNumber, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_VAT_NUMBER)))
             .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_PHONE_NUMBER)))
             .ForMember(d => d.Email, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, ClientsTable.COLUMN_EMAIL)));
        }
    }
}
