using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Inputs.LoanOffers;
using BankingAppDataTier.Contracts.Operations.Outputs.LoanOffers;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.LoanOffers;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.LoanOffers;

public class GetLoanOfferByIdTests : OperationTest<GetLoanOfferByIdOperation, GetLoanOfferByIdInput, GetLoanOfferByIdOutput>
{
    private IDatabaseLoanOfferProvider databaseLoanOfferProvider { get; set; }

    public GetLoanOfferByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetLoanOfferByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoanOfferProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>()!;
    }

    [Theory]
    [InlineData("Permanent_AU_01")]
    [InlineData("Permanent_MO_01")]
    [InlineData("Permanent_PE_01")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetLoanOfferByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.LoanOffer != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetLoanOfferByIdInput
        {
            Id = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });
       
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
