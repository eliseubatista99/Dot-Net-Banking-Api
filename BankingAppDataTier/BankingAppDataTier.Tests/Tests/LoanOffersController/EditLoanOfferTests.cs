using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.LoanOffers;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.LoanOffers;

public class EditLoanOfferTests : OperationTest<EditLoanOfferOperation, EditLoanOfferInput, VoidOperationOutput>
{
    private IDatabaseLoanOfferProvider databaseLoanOfferProvider { get; set; }

    public EditLoanOfferTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditLoanOfferOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoanOfferProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newName = "NewName";

        var editResponse = await SimulateOperationToTestCall(new EditLoanOfferInput
        {
            Id = "To_Edit_AU_01",
            Name = newName,
            Description = "new Desc",
            MaxEffort = 50,
            Interest = 12.0M,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = databaseLoanOfferProvider.GetById("To_Edit_AU_01");

        Assert.True(getByIdResponse?.Name == newName);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new EditLoanOfferInput
        {
            Id = "invalid_id",
            Description = "new Desc",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
