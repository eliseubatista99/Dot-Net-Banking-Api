using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppPresentationTier
{
    public class BankingAppPresentationTierOperationsBuilder : OperationsBuilder
    {
        
        //private void MapTransactionsOperations(ref WebApplication app, IApplicationContext context)
        //{
        //    MapPostOperation<AddTransactionOperation, AddTransactionInput, VoidOperationOutput>(ref app, context, new AddTransactionOperation(context, "/AddTransaction"));

        //    MapPostOperation<DeleteTransactionOperation, DeleteTransactionInput, VoidOperationOutput>(ref app, context, new DeleteTransactionOperation(context, "/DeleteTransaction"));

        //    MapPostOperation<EditTransactionOperation, EditTransactionInput, VoidOperationOutput>(ref app, context, new EditTransactionOperation(context, "/EditTransaction"));

        //    MapPostOperation<GetTransactionByIdOperation, GetTransactionByIdInput, GetTransactionByIdOutput>(ref app, context, new GetTransactionByIdOperation(context, "/GetTransactionById"));

        //    MapPostOperation<GetTransactionsOfClientOperation, GetTransactionsOfClientInput, GetTransactionsOfClientOutput>(ref app, context, new GetTransactionsOfClientOperation(context, "/GetTransactionsOfClient"));
        //}


        public override void MapOperations(ref WebApplication app, IApplicationContext context)
        {
            base.MapOperations(ref app, context);

            //MapAuthenticationOperations(ref app, context);
            //MapAccountsOperations(ref app, context);
            //MapCardsOperations(ref app, context);
            //MapClientsOperations(ref app, context);
            //MapLoanOffersOperations(ref app, context);
            //MapLoansOperations(ref app, context);
            //MapPlasticsOperations(ref app, context);
            //MapTransactionsOperations(ref app, context);
        }
    }
}
