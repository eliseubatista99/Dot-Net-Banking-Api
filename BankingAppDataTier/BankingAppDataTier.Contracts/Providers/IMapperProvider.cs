namespace BankingAppDataTier.Contracts.Providers
{
    public interface IMapperProvider
    {
        public Target Map<Source, Target>(Source source);

    }
}
