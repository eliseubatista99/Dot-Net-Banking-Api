namespace BankingAppPresentationTier
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = new BankingAppPresentationTierApplication();

            app.InitializeApp(builder);
        }
    }
}
