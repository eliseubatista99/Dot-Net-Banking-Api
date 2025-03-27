using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.LoanOffers;

public class DeleteLoanOfferTests : OperationTest<DeleteLoanOfferOperation, DeleteLoanOfferInput, VoidOperationOutput>
{
    private IDatabaseLoanOfferProvider databaseLoanOfferProvider { get; set; }

    public DeleteLoanOfferTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteLoanOfferOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoanOfferProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var delResponse = await SimulateOperationToTestCall(new DeleteLoanOfferInput
        {
            Id = "To_Delete_AU_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(delResponse.Error == null);

        var getByIdResponse = databaseLoanOfferProvider.GetById("To_Delete_AU_01");

        Assert.True(getByIdResponse == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new DeleteLoanOfferInput
        {
            Id = "Invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_CantDeleteWithRelatedLoans()
    {
        var response = await SimulateOperationToTestCall(new DeleteLoanOfferInput
        {
            Id = "Permanent_AU_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == LoanOffersErrors.CantDeleteWithRelatedLoans.Code);
    }
}
