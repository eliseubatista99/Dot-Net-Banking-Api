using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Database;

namespace BankingAppDataTier
{
    class Program
    {
        static void InjectDependencies(ref WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
        }


        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Cors
            builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            InjectDependencies(ref builder);
            var databaseInitializer = new DatabaseInitializer(builder.Services.BuildServiceProvider().GetService<IDatabaseProvider>());

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
