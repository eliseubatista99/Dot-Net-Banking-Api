using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Inputs.Accounts;
using BankingAppDataTier.Contracts.Operations.Outputs.Accounts;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Accounts;

public class GetAccountsByIdTests : OperationTest<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>
{
    public GetAccountsByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetAccountByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Theory]
    [InlineData("Permanent_Current_01")]
    [InlineData("Permanent_Savings_01")]
    [InlineData("Permanent_Investements_01")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetAccountByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Account != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetAccountByIdInput
        {
            Id = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Account == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
