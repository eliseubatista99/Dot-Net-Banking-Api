using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Operations.Authentication;
using BankingAppDataTier.Operations.Cards;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;

namespace BankingAppDataTier
{
    public class BankingAppDataTierOperationsBuilder : OperationsBuilder
    {
        private void MapAuthenticationOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AuthenticateOperation, AuthenticateInput, AuthenticateOutput>(ref app, context, new AuthenticateOperation(context, "/Authenticate"));

            MapPostOperation<GetAuthenticationPositionsOperation, GetAuthenticationPositionsInput, GetAuthenticationPositionsOutput>(ref app, context, new GetAuthenticationPositionsOperation(context, "/GetAuthenticationPositions"));

            MapPostOperation<IsValidTokenOperation, IsValidTokenInput, IsValidTokenOutput>(ref app, context, new IsValidTokenOperation(context, "/IsValidToken"));

            MapPostOperation<KeepAliveOperation, KeepAliveInput, KeepAliveOutput>(ref app, context, new KeepAliveOperation(context, "/KeepAlive"));
        }

        private void MapAccountsOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AddAccountOperation, AddAccountInput, VoidOperationOutput>(ref app, context, new AddAccountOperation(context, "/AddAccount"));

            MapPostOperation<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>(ref app, context, new DeleteAccountOperation(context, "/DeleteAccount"));

            MapPostOperation<EditAccountOperation, EditAccountInput, VoidOperationOutput>(ref app, context, new EditAccountOperation(context, "/EditAccount"));

            MapPostOperation<GetClientAccountsOperation, GetClientAccountsInput, GetClientAccountsOutput>(ref app, context, new GetClientAccountsOperation(context, "/GetClientAccounts"));

            MapPostOperation<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(ref app, context, new GetAccountByIdOperation(context, "/GetAccountById"));
        }

        private void MapCardsOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AddCardOperation, AddCardInput, VoidOperationOutput>(ref app, context, new AddCardOperation(context, "/AddCard"));

            MapPostOperation<DeleteCardOperation, DeleteCardInput, VoidOperationOutput>(ref app, context, new DeleteCardOperation(context, "/DeleteCard"));

            MapPostOperation<EditCardOperation, EditCardInput, VoidOperationOutput>(ref app, context, new EditCardOperation(context, "/EditCard"));

            MapPostOperation<GetCardByIdOperation, GetCardByIdInput, GetCardByIdOutput>(ref app, context, new GetCardByIdOperation(context, "/GetCardById"));

            MapPostOperation<GetCardsOfAccountOperation, GetCardsOfAccountInput, GetCardsOfAccountOutput>(ref app, context, new GetCardsOfAccountOperation(context, "/GetCardsOfAccount"));
        }

        public override void MapOperations(ref WebApplication app, IApplicationContext context)
        {
            base.MapOperations(ref app, context);

            MapAuthenticationOperations(ref app, context);
            MapAccountsOperations(ref app, context);
            MapCardsOperations(ref app, context);
        }
    }
}
