﻿using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Operations;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;
using AuthenticationTier = BankingAppAuthenticationTier.Contracts.Operations;

namespace BankingAppDataTier
{
    public class BankingAppDataTierOperationsBuilder : OperationsBuilder
    {
        private void MapAuthenticationOperations(ref WebApplication app, IApplicationContext context) 
        { 
            MapPostOperation<AuthenticateOperation, AuthenticationTier.AuthenticateInput, AuthenticationTier.AuthenticateOutput>(ref app, context, new AuthenticateOperation(context, "/Authenticate"));
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

        private void MapClientsOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AddClientOperation, AddClientInput, VoidOperationOutput>(ref app, context, new AddClientOperation(context, "/AddClient"));

            MapPostOperation<ChangePasswordOperation, ChangeClientPasswordInput, VoidOperationOutput>(ref app, context, new ChangePasswordOperation(context, "/ChangePassword"));

            MapPostOperation<DeleteClientOperation, DeleteClientInput, VoidOperationOutput>(ref app, context, new DeleteClientOperation(context, "/DeletClient"));

            MapPostOperation<EditClientOperation, EditClientInput, VoidOperationOutput>(ref app, context, new EditClientOperation(context, "/EditClient"));

            MapPostOperation<GetClientByIdOperation, GetClientByIdInput, GetClientByIdOutput>(ref app, context, new GetClientByIdOperation(context, "/GetClientById"));

            MapPostOperation<GetClientsOperation, VoidOperationInput, GetClientsOutput>(ref app, context, new GetClientsOperation(context, "/GetClients"));
        }

        private void MapLoanOffersOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<ActivateOrDeactivateLoanOfferOperation, ActivateOrDeactivateLoanOfferInput, VoidOperationOutput>(ref app, context, new ActivateOrDeactivateLoanOfferOperation(context, "/ActivateOrDeactivateLoanOffer"));

            MapPostOperation<AddLoanOfferOperation, AddLoanOfferInput, VoidOperationOutput>(ref app, context, new AddLoanOfferOperation(context, "/AddLoanOffer"));

            MapPostOperation<DeleteLoanOfferOperation, DeleteLoanOfferInput, VoidOperationOutput>(ref app, context, new DeleteLoanOfferOperation(context, "/DeleteLoanOffer"));

            MapPostOperation<EditLoanOfferOperation, EditLoanOfferInput, VoidOperationOutput>(ref app, context, new EditLoanOfferOperation(context, "/EditLoanOffer"));

            MapPostOperation<GetLoanOfferByIdOperation, GetLoanOfferByIdInput, GetLoanOfferByIdOutput>(ref app, context, new GetLoanOfferByIdOperation(context, "/GetLoanOfferById"));

            MapPostOperation<GetLoanOfferByTypeOperation, GetLoanOfferByTypeInput, GetLoanOffersByTypeOutput>(ref app, context, new GetLoanOfferByTypeOperation(context, "/GetLoanOfferByType"));
        }

        private void MapLoansOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AddLoanOperation, AddLoanInput, VoidOperationOutput>(ref app, context, new AddLoanOperation(context, "/AddLoan"));

            MapPostOperation<AmortizeLoanOperation, AmortizeLoanInput, VoidOperationOutput>(ref app, context, new AmortizeLoanOperation(context, "/AmortizeLoan"));

            MapPostOperation<DeleteLoanOperation, DeleteLoanInput, VoidOperationOutput>(ref app, context, new DeleteLoanOperation(context, "/DeleteLoan"));

            MapPostOperation<EditLoanOperation, EditLoanInput, VoidOperationOutput>(ref app, context, new EditLoanOperation(context, "/EditLoan"));

            MapPostOperation<GetLoanByIdOperation, GetLoanByIdInput, GetLoanByIdOutput>(ref app, context, new GetLoanByIdOperation(context, "/GetLoanById"));

            MapPostOperation<GetLoansOfAccountOperation, GetLoansOfAccountInput, GetLoansOfAccountOutput>(ref app, context, new GetLoansOfAccountOperation(context, "/GetLoansOfAccount"));
            
            MapPostOperation<GetLoansOfClientOperation, GetLoansOfClientInput, GetLoansOfClientOutput>(ref app, context, new GetLoansOfClientOperation(context, "/GetLoansOfClient"));
        }

        private void MapPlasticsOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<ActivateOrDeactivatePlasticOperation, ActivateOrDeactivatePlasticInput, VoidOperationOutput>(ref app, context, new ActivateOrDeactivatePlasticOperation(context, "/ActivateOrDeactivatePlastic"));

            MapPostOperation<AddPlasticOperation, AddPlasticInput, VoidOperationOutput>(ref app, context, new AddPlasticOperation(context, "/AddPlastic"));

            MapPostOperation<DeletePlasticOperation, DeletePlasticInput, VoidOperationOutput>(ref app, context, new DeletePlasticOperation(context, "/DeletePlastic"));

            MapPostOperation<EditPlasticOperation, EditPlasticInput, VoidOperationOutput>(ref app, context, new EditPlasticOperation(context, "/EditPlastic"));

            MapPostOperation<GetPlasticByIdOperation, GetPlasticByIdInput, GetPlasticByIdOutput>(ref app, context, new GetPlasticByIdOperation(context, "/GetPlasticById"));

            MapPostOperation<GetPlasticsOfTypeOperation, GetPlasticOfTypeInput, GetPlasticsOfTypeOutput>(ref app, context, new GetPlasticsOfTypeOperation(context, "/GetPlasticsOfType"));
        }

        private void MapTransactionsOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AddTransactionOperation, AddTransactionInput, VoidOperationOutput>(ref app, context, new AddTransactionOperation(context, "/AddTransaction"));

            MapPostOperation<DeleteTransactionOperation, DeleteTransactionInput, VoidOperationOutput>(ref app, context, new DeleteTransactionOperation(context, "/DeleteTransaction"));

            MapPostOperation<EditTransactionOperation, EditTransactionInput, VoidOperationOutput>(ref app, context, new EditTransactionOperation(context, "/EditTransaction"));

            MapPostOperation<GetTransactionByIdOperation, GetTransactionByIdInput, GetTransactionByIdOutput>(ref app, context, new GetTransactionByIdOperation(context, "/GetTransactionById"));

            MapPostOperation<GetTransactionsOfClientOperation, GetTransactionsOfClientInput, GetTransactionsOfClientOutput>(ref app, context, new GetTransactionsOfClientOperation(context, "/GetTransactionsOfClient"));
        }


        public override void MapOperations(ref WebApplication app, IApplicationContext context)
        {
            base.MapOperations(ref app, context);

            MapAuthenticationOperations(ref app, context);
            MapAccountsOperations(ref app, context);
            MapCardsOperations(ref app, context);
            MapClientsOperations(ref app, context);
            MapLoanOffersOperations(ref app, context);
            MapLoansOperations(ref app, context);
            MapPlasticsOperations(ref app, context);
            MapTransactionsOperations(ref app, context);
        }
    }
}
