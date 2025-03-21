﻿using AutoMapper;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Database;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TransactionsMapperProfile : Profile
    {

        public TransactionsMapperProfile()
        {
            this.CreateMapOfEntities();
        }


        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<TransactionTableEntry, TransactionDto>()
             .ForMember(d => d.Role, opt => opt.MapFrom(s => TransactionRole.None));

            this.CreateMap<TransactionDto, TransactionTableEntry>();

            this.CreateMap<NpgsqlDataReader, TransactionTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_ID)))
             .ForMember(d => d.TransactionDate, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_TRANSACTION_DATE)))
             .ForMember(d => d.Amount, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_AMOUNT)))
             .ForMember(d => d.Urgent, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_URGENT)))
             .ForMember(d => d.DestinationName, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_DESTINATION_NAME)))
             .ForMember(d => d.Description, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_DESCRIPTION)))
             .ForMember(d => d.Fees, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_FEES)))
             .ForMember(d => d.SourceAccount, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_SOURCE_ACCOUNT)))
             .ForMember(d => d.DestinationAccount, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_DESTINATION_ACCOUNT)))
             .ForMember(d => d.SourceCard, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, TransactionsTable.COLUMN_SOURCE_CARD)));
        }
    }
}
