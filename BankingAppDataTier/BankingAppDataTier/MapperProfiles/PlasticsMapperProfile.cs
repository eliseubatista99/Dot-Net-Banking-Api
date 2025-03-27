using AutoMapper;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Database;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlasticsMapperProfile : Profile
    {

        public PlasticsMapperProfile()
        {
            this.CreateMapOfEnums();
            this.CreateMapOfEntities();
        }

        /// <summary>
        /// Create map of enums.
        /// </summary>
        private void CreateMapOfEnums()
        {
            this.CreateMap<string, CardType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierConstants.CARD_TYPE_DEBIT => CardType.Debit,
                    BankingAppDataTierConstants.CARD_TYPE_CREDIT => CardType.Credit,
                    BankingAppDataTierConstants.CARD_TYPE_PRE_PAID => CardType.PrePaid,
                    BankingAppDataTierConstants.CARD_TYPE_MEAL => CardType.Meal,
                    _ => CardType.None,
                };
            });

            this.CreateMap<CardType, string>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    CardType.Debit => BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                    CardType.Credit => BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                    CardType.PrePaid => BankingAppDataTierConstants.CARD_TYPE_PRE_PAID,
                    CardType.Meal => BankingAppDataTierConstants.CARD_TYPE_MEAL,
                    _ => ""
                };
            });
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<PlasticTableEntry, PlasticDto>();

            this.CreateMap<PlasticDto, PlasticTableEntry>()
             .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive.GetValueOrDefault()));

            this.CreateMap<NpgsqlDataReader, PlasticTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_ID)))
             .ForMember(d => d.CardType, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_TYPE)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_NAME)))
             .ForMember(d => d.Image, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_IMAGE)))
             .ForMember(d => d.Cashback, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_CASHBACK)))
             .ForMember(d => d.Commission, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_COMISSION)))
             .ForMember(d => d.IsActive, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, PlasticsTable.COLUMN_IS_ACTIVE)));
        }
    }
}
