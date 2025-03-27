namespace BankingAppAuthenticationTier
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = new BankingAppAuthenticationTierApplication();

            app.InitializeApp(builder);
        }
    }
}
