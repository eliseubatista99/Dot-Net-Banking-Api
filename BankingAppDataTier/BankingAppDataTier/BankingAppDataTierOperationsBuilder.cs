using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Controllers.Accounts;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;

namespace BankingAppDataTier
{
    public class BankingAppDataTierOperationsBuilder : OperationsBuilder
    {
        public override void MapOperations(ref WebApplication app, IApplicationContext context)
        {
            base.MapOperations(ref app, context);

            MapPostOperation<AddAccountOperation, AddAccountInput, VoidOperationOutput>(ref app, context, new AddAccountOperation(context, "/AddAccount"));

            MapPostOperation<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>(ref app, context, new DeleteAccountOperation(context, "/DeleteAccount"));

            MapPostOperation<EditAccountOperation, EditAccountInput, VoidOperationOutput>(ref app, context, new EditAccountOperation(context, "/EditAccount"));

            MapPostOperation<GetClientAccountsOperation, GetClientAccountsInput, GetClientAccountsOutput>(ref app, context, new GetClientAccountsOperation(context, "/GetClientAccounts"));

            MapPostOperation<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(ref app, context, new GetAccountByIdOperation(context, "/GetAccountById"));
        }
    }
}
