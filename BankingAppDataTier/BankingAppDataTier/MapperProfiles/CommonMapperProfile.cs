using AutoMapper;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Database;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;

namespace BankingAppDataTier.MapperProfiles
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class CommonMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonMapperProfile"/> class.
        /// </summary>
        public CommonMapperProfile()
        {
            this.CreateMapOfEnums();
            this.CreateMapOfCardEntities();
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
        /// Create map of card entities.
        /// </summary>
        private void CreateMapOfCardEntities()
        {
            this.CreateMap<CardsTableEntry, CardDto>()
             .ForMember(d => d.CardType, opt => opt.MapFrom(s => CardType.None))
             .ForMember(d => d.Image, opt => opt.MapFrom(s => string.Empty));

            this.CreateMap<CardDto, CardsTableEntry>();

            this.CreateMap<NpgsqlDataReader, CardsTableEntry>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_ID)))
             .ForMember(d => d.RelatedAccountID, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_RELATED_ACCOUNT_ID)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_NAME)))
             .ForMember(d => d.PlasticId, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_PLASTIC_ID)))
             .ForMember(d => d.RequestDate, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_REQUEST_DATE)))
             .ForMember(d => d.ActivationDate, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_ACTIVATION_DATE)))
             .ForMember(d => d.ExpirationDate, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_EXPIRATION_DATE)))
             .ForMember(d => d.Balance, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_BALANCE)))
             .ForMember(d => d.PaymentDay, opt => opt.MapFrom(s => SqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_PAYMENT_DAY)));
        }

    }
}
