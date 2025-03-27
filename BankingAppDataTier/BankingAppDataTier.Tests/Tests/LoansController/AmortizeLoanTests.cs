using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Loans;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Loans;

public class AmortizeLoanTests : OperationTest<AmortizeLoanOperation, AmortizeLoanInput, VoidOperationOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public AmortizeLoanTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AmortizeLoanOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var amortizeResponse = await SimulateOperationToTestCall(new AmortizeLoanInput
        {
            Id = "Permanent_AU_01",
            Amount = 100,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(amortizeResponse.Error == null);

        var getByIdResponse = databaseLoansProvider.GetById("Permanent_AU_01");

        Assert.True(getByIdResponse?.PaidAmount == 100);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new AmortizeLoanInput
        {
            Id = "invalid_id",
            Amount = 100,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_InsufficientFunds()
    {
        var response = await SimulateOperationToTestCall(new AmortizeLoanInput
        {
            Id = "Permanent_AU_01",
            Amount = 999999,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == LoansErrors.InsufficientFunds.Code);
    }
}
