namespace BankingAppDataTier
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = new BankingAppDataTierApplication();

            app.InitializeApp(builder);
        }
    }
}
