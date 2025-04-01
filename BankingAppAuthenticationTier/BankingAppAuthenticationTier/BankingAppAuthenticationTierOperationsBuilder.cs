using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Operations;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppAuthenticationTier
{
    public class BankingAppAuthenticationTierOperationsBuilder : OperationsBuilder
    {
        private void MapAuthenticationOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<AuthenticateOperation, AuthenticateInput, AuthenticateOutput>(ref app, context, new AuthenticateOperation(context, "/Authenticate"));

            MapPostOperation<GetAuthenticationPositionsOperation, GetAuthenticationPositionsInput, GetAuthenticationPositionsOutput>(ref app, context, new GetAuthenticationPositionsOperation(context, "/GetAuthenticationPositions"));

            MapPostOperation<IsValidTokenOperation, IsValidTokenInput, IsValidTokenOutput>(ref app, context, new IsValidTokenOperation(context, "/IsValidToken"));

            MapPostOperation<RefreshTokenOperation, RefreshTokenInput, RefreshTokenOutput>(ref app, context, new RefreshTokenOperation(context, "/RefreshToken"));
        }

        public override void MapOperations(ref WebApplication app, IApplicationContext context)
        {
            base.MapOperations(ref app, context);

            MapAuthenticationOperations(ref app, context);
        }
    }
}
