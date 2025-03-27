using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppBusinessTier.Contracts.Enums;
using BankingAppBusinessTier.Contracts.Operations.Cards;
using ExternalApplications.DataTier.Providers;
using BankingAppDataTierOperations = BankingAppDataTier.Contracts.Operations;
using BankingAppDataTierEnums = BankingAppDataTier.Contracts.Enums;

namespace BankingAppBusinessTier.Operations.Cards
{
    public class GetEligiblePlasticsOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetEligiblePlasticsInput, GetEligiblePlasticsOutput>(context, endpoint)
    {
        private IDataTierProvider dataTierProvider;
        //private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            dataTierProvider = executionContext.GetDependency<IDataTierProvider>()!;
            //databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(GetEligiblePlasticsInput input)
        {
            var baseValidation = await base.ValidateInput(input);

            if (baseValidation.Error == null)
            {
                if (input.ClientId == null)
                {
                    return (HttpStatusCode.BadRequest, InputErrors.InvalidInputField(nameof(input.ClientId)));
                }
                if (input.PlasticType == CardType.None)
                {
                    return (HttpStatusCode.BadRequest, InputErrors.InvalidInputField(nameof(input.PlasticType)));
                }
            }

            return baseValidation;
        }

        protected override async Task<GetEligiblePlasticsOutput> ExecuteAsync(GetEligiblePlasticsInput input)
        {
            var plasticType = mapperProvider.Map<CardType, BankingAppDataTierEnums.CardType>(input.PlasticType);

            var getPlasticOfTypeAwaiter = dataTierProvider.GetPlasticOfType(new BankingAppDataTierOperations.GetPlasticOfTypeInput
            {
                PlasticType = plasticType,
                Metadata = input.Metadata,
            }).ConfigureAwait(false);

            var getPlasticOfTypeResponse = await getPlasticOfTypeAwaiter;

            return new GetEligiblePlasticsOutput
            {
                Plastics = new List<Contracts.Dtos.PlasticDto>(),

            };
        }
    }
}
