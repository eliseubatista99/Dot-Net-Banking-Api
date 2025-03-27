using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using BankingAppBusinessTier.Contracts.Enums;
using DataTier = BankingAppDataTier.Contracts;

namespace BankingAppBusinessTier.MapperProfiles
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
            this.CreateMap<DataTier.Enums.AccountType, AccountType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    DataTier.Enums.AccountType.Current => AccountType.Current,
                    DataTier.Enums.AccountType.Investments => AccountType.Investments,
                    DataTier.Enums.AccountType.Savings => AccountType.Savings,
                    _ => AccountType.None
                };
            });

            this.CreateMap<AccountType, DataTier.Enums.AccountType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    AccountType.Current => DataTier.Enums.AccountType.Current,
                    AccountType.Investments => DataTier.Enums.AccountType.Investments,
                    AccountType.Savings => DataTier.Enums.AccountType.Savings,
                    _ => DataTier.Enums.AccountType.None
                };
            });

            this.CreateMap<DataTier.Enums.CardType, CardType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    DataTier.Enums.CardType.Debit => CardType.Debit,
                    DataTier.Enums.CardType.Credit => CardType.Credit,
                    DataTier.Enums.CardType.PrePaid => CardType.PrePaid,
                    DataTier.Enums.CardType.Meal => CardType.Meal,
                    _ => CardType.None
                };
            });

            this.CreateMap<CardType, DataTier.Enums.CardType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    CardType.Debit => DataTier.Enums.CardType.Debit,
                    CardType.Credit => DataTier.Enums.CardType.Credit,
                    CardType.PrePaid => DataTier.Enums.CardType.PrePaid,
                    CardType.Meal => DataTier.Enums.CardType.Meal,
                    _ => DataTier.Enums.CardType.None
                };
            });

            this.CreateMap<DataTier.Enums.LoanType, LoanType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    DataTier.Enums.LoanType.Mortgage => LoanType.Mortgage,
                    DataTier.Enums.LoanType.Auto => LoanType.Auto,
                    DataTier.Enums.LoanType.Personal => LoanType.Personal,
                    _ => LoanType.None
                };
            });

            this.CreateMap<LoanType, DataTier.Enums.LoanType>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    LoanType.Mortgage => DataTier.Enums.LoanType.Mortgage,
                    LoanType.Auto => DataTier.Enums.LoanType.Auto,
                    LoanType.Personal => DataTier.Enums.LoanType.Personal,
                    _ => DataTier.Enums.LoanType.None
                };
            });

            this.CreateMap<DataTier.Enums.TransactionRole, TransactionRole>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    DataTier.Enums.TransactionRole.Sender => TransactionRole.Sender,
                    DataTier.Enums.TransactionRole.Receiver => TransactionRole.Receiver,
                    _ => TransactionRole.None
                };
            });

            this.CreateMap<TransactionRole, DataTier.Enums.TransactionRole>().ConvertUsing((src, _) =>
            {
                return src switch
                {
                    TransactionRole.Sender => DataTier.Enums.TransactionRole.Sender,
                    TransactionRole.Receiver => DataTier.Enums.TransactionRole.Receiver,
                    _ => DataTier.Enums.TransactionRole.None
                };
            });
        }

        /// <summary>
        /// Create map of account entities.
        /// </summary>
        private void CreateMapOfEntities()
        {
            //this.CreateMap<DataTier.Dtos, AuthenticationCodeItemDto>()
            // .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Position))
            // .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value));
              
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
