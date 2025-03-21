using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Controllers.Accounts;

namespace BankingAppDataTier
{
    public class BankingAppDataTierOperations
    {
        private static void MapAccountsOperations(ref WebApplication app)
        {
            app.MapPost("/AddAccount", (IExecutionContext context, AddAccountInput input) =>
               new AddAccountOperation(context).Call(input)).
               Produces<VoidOutput>();

            app.MapPost("/DeleteAccount", (IExecutionContext context, DeleteAccountInput input) =>
               new DeleteAccountOperation(context).Call(input)).
               Produces<VoidOutput>();

            app.MapPost("/EditAccount", (IExecutionContext context, EditAccountInput input) =>
               new EditAccountOperation(context).Call(input)).
               Produces<VoidOutput>();

            app.MapPost("/GetClientAccounts", (IExecutionContext context, GetClientAccountsInput input) =>  
                new GetClientAccountsOperation(context).Call(input)).
                Produces<GetClientAccountsOutput>();

            app.MapPost("/GetAccountById", (IExecutionContext context, GetAccountByIdInput input) =>
               new GetAccountByIdOperation(context).Call(input)).
               Produces<GetAccountByIdOutput>();
        }

        public static void MapOperations(ref WebApplication app) 
        {
            MapAccountsOperations(ref app);
        }
    }
}
