using AutoMapper;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Database;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
             .ForMember(d => d.Id, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_ID)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_NAME)))
             .ForMember(d => d.StartDate, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_START_DATE)))
             .ForMember(d => d.RelatedAccount, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_RELATED_ACCOUNT)))
             .ForMember(d => d.RelatedOffer, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_RELATED_OFFER)))
             .ForMember(d => d.Duration, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_DURATION)))
             .ForMember(d => d.ContractedAmount, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_CONTRACTED_AMOUNT)))
             .ForMember(d => d.PaidAmount, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, LoansTable.COLUMN_PAID_AMOUNT)));
        }
    }
}
