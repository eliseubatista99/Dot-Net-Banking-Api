namespace BankingAppBusinessTier
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = new BankingAppBusinessTierApplication();

            app.InitializeApp(builder);
        }
    }
}
