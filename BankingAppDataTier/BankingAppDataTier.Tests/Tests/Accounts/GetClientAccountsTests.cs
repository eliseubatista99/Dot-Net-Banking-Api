using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests.Helpers;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Accounts;

public class GetClientAccountsTests : OperationTest<GetClientAccountsOperation, GetClientAccountsInput, GetClientAccountsOutput>
{
    public GetClientAccountsTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetClientAccountsOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Theory]
    [InlineData("Permanent_Current_01")]
    [InlineData("Permanent_Savings_01")]
    [InlineData("Permanent_Investements_01")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetClientAccountsInput
        {
            ClientId = "Permanent_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Accounts.Count > 0);
    }
}
