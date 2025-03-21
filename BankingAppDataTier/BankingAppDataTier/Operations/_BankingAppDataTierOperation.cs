using BankingAppDataTier.Contracts.Dtos.Inputs;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using System.Net;


namespace BankingAppDataTier.Operations
{
    public class _BankingAppDataTierOperation<TIn, TOut> where TIn : _BaseInput where TOut : _BaseOutput
    {
        protected IExecutionContext executionContext;
        protected IAuthenticationProvider authenticationProvider;

        protected virtual bool NeedsAuthorization { get; set; } = true;

        public _BankingAppDataTierOperation(IExecutionContext context)
        {
            executionContext = context;
        }

        protected virtual bool IsAuthorized(_BaseInput input)
        {
            if (input.Metadata?.Token == null)
            {
                return false;
            }

            var tokenValidationResult = authenticationProvider.IsValidToken(input.Metadata.Token);

            return tokenValidationResult.isValid;
        }

        protected virtual async Task InitAsync()
        {
            authenticationProvider = executionContext.GetDependency<IAuthenticationProvider>()!;
        }

        protected virtual async Task<TOut> ExecuteAsync(TIn input)
        {
            return default(TOut);
        }

        public virtual async Task<IResult> Call(TIn input)
        {
            await InitAsync();

            if (NeedsAuthorization && !IsAuthorized(input))
            {
                //return Results.Unauthorized();
                return new OperationResultDto(HttpStatusCode.Unauthorized);
            }

            var executionResponse = await ExecuteAsync(input).ConfigureAwait(false);

            if(executionResponse == null)
            {
                return new OperationResultDto(new _BaseOutput 
                { 
                    StatusCode = HttpStatusCode.NoContent
                });
            }

            if (executionResponse.StatusCode == null)
            {
                executionResponse.StatusCode = HttpStatusCode.OK;
            }

            return new OperationResultDto(executionResponse);

        }
    }
}
