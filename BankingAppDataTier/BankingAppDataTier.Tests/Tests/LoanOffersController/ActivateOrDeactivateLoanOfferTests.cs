using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.LoanOffers;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.LoanOffers;

public class ActivateOrDeactivateLoanOfferTests : OperationTest<ActivateOrDeactivateLoanOfferOperation, ActivateOrDeactivateLoanOfferInput, VoidOperationOutput>
{
    private IDatabaseLoanOfferProvider databaseLoanOfferProvider { get; set; }

    public ActivateOrDeactivateLoanOfferTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new ActivateOrDeactivateLoanOfferOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoanOfferProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var activateResponse = await SimulateOperationToTestCall(new ActivateOrDeactivateLoanOfferInput
        {
            Id = "To_Edit_AU_02",
            Active = false,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(activateResponse.Error == null);

        var getByTypeResponse = databaseLoanOfferProvider.GetByType(BankingAppDataTierConstants.LOAN_TYPE_AUTO, true);

        Assert.True(getByTypeResponse.Find(l => l.Id == "To_Edit_AU_02") == null);

        activateResponse = await SimulateOperationToTestCall(new ActivateOrDeactivateLoanOfferInput
        {
            Id = "To_Edit_AU_02",
            Active = true,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(activateResponse.Error == null);

        getByTypeResponse = databaseLoanOfferProvider.GetByType(BankingAppDataTierConstants.LOAN_TYPE_AUTO);

        Assert.True(getByTypeResponse.Find(l => l.Id == "To_Edit_AU_02") != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new ActivateOrDeactivateLoanOfferInput
        {
            Id = "invalidId",
            Active = false,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
