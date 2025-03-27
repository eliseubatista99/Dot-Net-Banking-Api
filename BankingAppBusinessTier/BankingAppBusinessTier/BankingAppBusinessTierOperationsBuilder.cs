
using BankingAppBusinessTier.Contracts.Operations.Cards;
using BankingAppBusinessTier.Operations.Cards;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppBusinessTier
{
    public class BankingAppBusinessTierOperationsBuilder : OperationsBuilder
    {

        private void MapCardsOperations(ref WebApplication app, IApplicationContext context)
        {
            MapPostOperation<GetEligiblePlasticsOperation, GetEligiblePlasticsInput, GetEligiblePlasticsOutput>(ref app, context, new GetEligiblePlasticsOperation(context, "/GetEligiblePlastics"));
        }


        public override void MapOperations(ref WebApplication app, IApplicationContext context)
        {
            base.MapOperations(ref app, context);

            MapCardsOperations(ref app, context);
        }
    }
}
