using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Constants;
using BankingAppAuthenticationTier.Library.Errors;
using BankingAppAuthenticationTier.Library.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppAuthenticationTier.Operations
{
    public class GetAuthenticationPositionsOperation(IApplicationContext context, string endpoint)
        : BankingAppAuthenticationTierOperation<GetAuthenticationPositionsInput, GetAuthenticationPositionsOutput>(context, endpoint)
    {
        protected override bool UseAuthentication { get; set; } = false;

        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<GetAuthenticationPositionsOutput> ExecuteAsync(GetAuthenticationPositionsInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return new GetAuthenticationPositionsOutput()
                {
                    Positions = new List<int>(),
                    Error = AuthenticationErrors.InvalidClient,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (clientInDb.Password.Length == 0)
            {
                return new GetAuthenticationPositionsOutput()
                {
                    Positions = new List<int>(),
                };
            }

            Random rnd = new Random();

            var numberOfPositions = input.NumberOfPositions ?? BankingAppAuthenticationTierConstants.DEDFAULT_NUMBER_OF_POSITIONS;

            var positions = Enumerable.Range(0, clientInDb.Password.Length).Select(x => (int)x).ToList();
            var result = new List<int>();

            for (int i = 0; i < numberOfPositions; i++)
            {
                //If there is no more to take, end the loop
                if (positions.Count == 0)
                {
                    break;
                }

                var randomPos = rnd.Next(0, positions.Count - 1);
                var newPos = positions[randomPos];

                result.Add(newPos);
                positions = positions.Where(p => p != newPos).ToList();
            }

            return new GetAuthenticationPositionsOutput()
            {
                Positions = result,
            };
        }
    }
}
