using AutoMapper;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.PostgreSql;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class CardsMapperProfile : Profile
    {
        public CardsMapperProfile()
        {
            this.CreateMapOfCardEntities();
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
             .ForMember(d => d.Id, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_ID)))
             .ForMember(d => d.RelatedAccountID, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_RELATED_ACCOUNT_ID)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_NAME)))
             .ForMember(d => d.PlasticId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_PLASTIC_ID)))
             .ForMember(d => d.RequestDate, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_REQUEST_DATE)))
             .ForMember(d => d.ActivationDate, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_ACTIVATION_DATE)))
             .ForMember(d => d.ExpirationDate, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_EXPIRATION_DATE)))
             .ForMember(d => d.Balance, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_BALANCE)))
             .ForMember(d => d.PaymentDay, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, CardsTable.COLUMN_PAYMENT_DAY)));
        }

    }
}
