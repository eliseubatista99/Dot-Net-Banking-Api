using AutoMapper;
using BankingAppDataTierDtos = BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTierEnums = BankingAppDataTier.Contracts.Enums;
using System.Diagnostics.CodeAnalysis;
using BankingAppBusinessTier.Contracts.Enums;
using BankingAppBusinessTier.Dtos;

namespace ExternalApplications.DataTier.MapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class DataTierMapperProfile : Profile
    {

        public DataTierMapperProfile()
        {
            this.CreateMapOfEnums();
            this.CreateMapOfEntities();
        }

        /// <summary>
        /// Create map of enums.
        /// </summary>
        private void CreateMapOfEnums()
        {
            this.CreateMap<BankingAppDataTierEnums.AccountType, AccountType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierEnums.AccountType.Current => AccountType.Current,
                    BankingAppDataTierEnums.AccountType.Investments => AccountType.Investments,
                    BankingAppDataTierEnums.AccountType.Savings => AccountType.Savings,
                    _ => AccountType.None
                };
            });

            this.CreateMap<AccountType ,BankingAppDataTierEnums.AccountType> ().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    AccountType.Current => BankingAppDataTierEnums.AccountType.Current,
                    AccountType.Investments => BankingAppDataTierEnums.AccountType.Investments,
                    AccountType.Savings => BankingAppDataTierEnums.AccountType.Savings,
                    _ => BankingAppDataTierEnums.AccountType.None
                };
            });

            this.CreateMap<BankingAppDataTierEnums.CardType, CardType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierEnums.CardType.Debit => CardType.Debit,
                    BankingAppDataTierEnums.CardType.Credit => CardType.Credit,
                    BankingAppDataTierEnums.CardType.PrePaid => CardType.PrePaid,
                    BankingAppDataTierEnums.CardType.Meal => CardType.Meal,
                    _ => CardType.None
                };
            });

            this.CreateMap<CardType, BankingAppDataTierEnums.CardType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    CardType.Debit => BankingAppDataTierEnums.CardType.Debit,
                    CardType.Credit => BankingAppDataTierEnums.CardType.Credit,
                    CardType.PrePaid => BankingAppDataTierEnums.CardType.PrePaid,
                    CardType.Meal => BankingAppDataTierEnums.CardType.Meal,
                    _ => BankingAppDataTierEnums.CardType.None
                };
            });

            this.CreateMap<BankingAppDataTierEnums.LoanType, LoanType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierEnums.LoanType.Mortgage => LoanType.Mortgage,
                    BankingAppDataTierEnums.LoanType.Auto => LoanType.Auto,
                    BankingAppDataTierEnums.LoanType.Personal => LoanType.Personal,
                    _ => LoanType.None
                };
            });

            this.CreateMap<LoanType, BankingAppDataTierEnums.LoanType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    LoanType.Mortgage => BankingAppDataTierEnums.LoanType.Mortgage,
                    LoanType.Auto => BankingAppDataTierEnums.LoanType.Auto,
                    LoanType.Personal => BankingAppDataTierEnums.LoanType.Personal,
                    _ => BankingAppDataTierEnums.LoanType.None
                };
            });

            this.CreateMap<BankingAppDataTierEnums.TransactionRole, TransactionRole>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    BankingAppDataTierEnums.TransactionRole.Sender => TransactionRole.Sender,
                    BankingAppDataTierEnums.TransactionRole.Receiver => TransactionRole.Receiver,
                    _ => TransactionRole.None
                };
            });

            this.CreateMap<TransactionRole, BankingAppDataTierEnums.TransactionRole>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    TransactionRole.Sender => BankingAppDataTierEnums.TransactionRole.Sender,
                    TransactionRole.Receiver => BankingAppDataTierEnums.TransactionRole.Receiver,
                    _ => BankingAppDataTierEnums.TransactionRole.None
                };
            });
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            this.CreateMap<BankingAppDataTierDtos.AuthenticationCodeItemDto, AuthenticationCodeItemDto>()
             .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Position))
             .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));

            //this.CreateMap<AccountDto, AccountsTableEntry>()
            // .ForMember(d => d.AccountId, opt => opt.MapFrom(s => s.Id));

            //this.CreateMap<NpgsqlDataReader, AccountsTableEntry>()
            // .ForMember(d => d.AccountId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_ID)))
            // .ForMember(d => d.OwnerCliendId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_OWNER_CLIENT_ID)))
            // .ForMember(d => d.AccountType, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_TYPE)))
            // .ForMember(d => d.Balance, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_BALANCE)))
            // .ForMember(d => d.Name, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_NAME)))
            // .ForMember(d => d.Image, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_IMAGE)))
            // .ForMember(d => d.SourceAccountId, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_SOURCE_ACCOUNT_ID)))
            // .ForMember(d => d.Duration, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_DURATION)))
            // .ForMember(d => d.Interest, opt => opt.MapFrom(s => NpgsqlDatabaseHelper.ReadColumnValue(s, AccountsTable.COLUMN_INTEREST)));
        }
    }
}
