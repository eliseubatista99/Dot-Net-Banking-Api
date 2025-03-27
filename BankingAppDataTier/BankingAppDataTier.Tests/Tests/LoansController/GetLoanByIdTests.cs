using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Loans;

public class GetLoanByIdTests : OperationTest<GetLoanByIdOperation, GetLoanByIdInput, GetLoanByIdOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public GetLoanByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetLoanByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }

    [Theory]
    [InlineData("Permanent_AU_01")]
    [InlineData("Permanent_MO_01")]
    [InlineData("Permanent_PE_01")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetLoanByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Loan != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetLoanByIdInput
        {
            Id = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
