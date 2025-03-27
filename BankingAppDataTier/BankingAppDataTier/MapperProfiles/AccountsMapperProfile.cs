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
    public class AccountsMapperProfile : Profile
    {

        public AccountsMapperProfile()
        {
            this.CreateMapOfEnums();
            this.CreateMapOfEntities();
        }

        /// <summary>
        /// Create map of enums.
        /// </summary>
        private void CreateMapOfEnums()
        {
            this.CreateMap<string, AccountType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT => AccountType.Current,
                    BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS => AccountType.Savings,
                    BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS => AccountType.Investments,
                    _ => AccountType.None
                };
            });

            this.CreateMap<AccountType, string>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    AccountType.Current => BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                    AccountType.Savings => BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS,
                    AccountType.Investments => BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS,
                    _ => ""
                };
            });
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<AccountsTableEntry, AccountDto>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AccountId));

            this.CreateMap<AccountDto, AccountsTableEntry>()
             .ForMember(d => d.AccountId, opt => opt.MapFrom(s => s.Id));

            this.CreateMap<NpgsqlDataReader, AccountsTableEntry>()
             .ForMember(d => d.AccountId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_ID)))
             .ForMember(d => d.OwnerCliendId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_OWNER_CLIENT_ID)))
             .ForMember(d => d.AccountType, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_TYPE)))
             .ForMember(d => d.Balance, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_BALANCE)))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_NAME)))
             .ForMember(d => d.Image, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_IMAGE)))
             .ForMember(d => d.SourceAccountId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_SOURCE_ACCOUNT_ID)))
             .ForMember(d => d.Duration, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_DURATION)))
             .ForMember(d => d.Interest, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_INTEREST)));
        }
    }
}
