using AutoMapper;
using BankingAppDataTier.Library.Constants.Database;
using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.PostgreSql;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class LoansMapperProfile : Profile
    {
        public LoansMapperProfile()
        {
            this.CreateMapOfCardEntities();
        }

        /// <summary>
        /// Create map of card entities.
        /// </summary>
        private void CreateMapOfCardEntities()
        {
            this.CreateMap<LoanTableEntry, LoanDto>()
             .ForMember(d => d.LoanType, opt => opt.MapFrom(s => LoanType.None))
             .ForMember(d => d.Interest, opt => opt.MapFrom(s => 0));

            this.CreateMap<LoanDto, LoanTableEntry>();

            this.CreateMap<NpgsqlDataReader, LoanTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_ID)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_NAME)))
             .ForMember(d => d.StartDate, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_START_DATE)))
             .ForMember(d => d.RelatedAccount, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_RELATED_ACCOUNT)))
             .ForMember(d => d.RelatedOffer, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_RELATED_OFFER)))
             .ForMember(d => d.Duration, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_DURATION)))
             .ForMember(d => d.ContractedAmount, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_CONTRACTED_AMOUNT)))
             .ForMember(d => d.PaidAmount, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_PAID_AMOUNT)));
        }
    }
}
