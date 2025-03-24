using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests.Helpers;
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
        var response = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(OperationToTest!, new GetAccountByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Account != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(OperationToTest!, new GetAccountByIdInput
        {
            Id = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Account == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
