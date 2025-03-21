namespace BankingAppDataTier.Contracts.Providers
{
    public interface IExecutionContext
    {
        public T? GetDependency<T>() where T : class;
    }
}
