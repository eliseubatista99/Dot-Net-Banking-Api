using AutoMapper;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.PostgreSql;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class LoanOffersMapperProfile : Profile
    {

        public LoanOffersMapperProfile()
        {
            this.CreateMapOfEnums();
            this.CreateMapOfEntities();
        }

        /// <summary>
        /// Create map of enums.
        /// </summary>
        private void CreateMapOfEnums()
        {
            this.CreateMap<string, LoanType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierConstants.LOAN_TYPE_AUTO => LoanType.Auto,
                    BankingAppDataTierConstants.LOAN_TYPE_MORTAGAGE => LoanType.Mortgage,
                    BankingAppDataTierConstants.LOAN_TYPE_PERSONAL => LoanType.Personal,
                    _ => LoanType.None,
                };
            });

            this.CreateMap<LoanType, string>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    LoanType.Auto => BankingAppDataTierConstants.LOAN_TYPE_AUTO,
                    LoanType.Mortgage => BankingAppDataTierConstants.LOAN_TYPE_MORTAGAGE,
                    LoanType.Personal => BankingAppDataTierConstants.LOAN_TYPE_PERSONAL,
                    _ => ""
                };
            });
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<LoanOfferTableEntry, LoanOfferDto>();

            this.CreateMap<LoanOfferDto, LoanOfferTableEntry>()
             .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive.GetValueOrDefault()));

            this.CreateMap<NpgsqlDataReader, LoanOfferTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_ID)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_NAME)))
             .ForMember(d => d.Description, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_DESCRIPTION)))
             .ForMember(d => d.LoanType, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_TYPE)))
             .ForMember(d => d.MaxEffort, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_MAX_EFFORT)))
             .ForMember(d => d.Interest, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_INTEREST)))
             .ForMember(d => d.IsActive, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, LoanOffersTable.COLUMN_IS_ACTIVE)));
        }
    }
}
