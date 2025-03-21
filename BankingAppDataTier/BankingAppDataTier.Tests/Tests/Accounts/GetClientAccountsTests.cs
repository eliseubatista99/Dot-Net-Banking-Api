using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Controllers.Accounts;
using BankingAppDataTier.Tests.Constants;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BankingAppDataTier.Tests.Accounts;

public class GetClientAccountsTests
{
    private GetClientAccountsOperation getClientAccountsOperation;

    private void Setup()
    {
        TestMocksBuilder.Mock();

        getClientAccountsOperation = new GetClientAccountsOperation(TestMocksBuilder._ExecutionContextMock);
    }

    [Fact]
    public async Task ShouldBe_SuccessAsync()
    {
        Setup();

        var result = (OperationResultDto) await getClientAccountsOperation.Call(new GetClientAccountsInput
        {
            ClientId = "Permanent_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        }).ConfigureAwait(false);

        var tipo = result.GetType();

        var response = (GetClientAccountsOutput)result.Output!;

        Assert.True(response.Accounts.Count > 0);
    }
}
