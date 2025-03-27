using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Loans;

public class EditLoanTests : OperationTest<EditLoanOperation, EditLoanInput, VoidOperationOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public EditLoanTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditLoanOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newName = "newName";

        var editResponse = await SimulateOperationToTestCall(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            Name = newName,
            RelatedOffer = "Permanent_AU_01",
            RelatedAccount = "Permanent_Current_01",
            Duration = 10,
            PaidAmount = 0,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = databaseLoansProvider.GetById("Permanent_AU_01");

        Assert.True(getByIdResponse?.Name == newName);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new EditLoanInput
        {
            Id = "invalid_id",
            Name = "name",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_CantChangeLoanType()
    {
        var response = await SimulateOperationToTestCall(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            RelatedOffer = "Permanent_MO_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == LoansErrors.CantChangeLoanType.Code);
    }


    [Fact]
    public async Task ShouldReturnError_InvalidRelatedOffer()
    {
        var response = await SimulateOperationToTestCall(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            RelatedOffer = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == LoansErrors.InvalidRelatedOffer.Code);
    }


    [Fact]
    public async Task ShouldReturnError_InvalidRelatedAccount()
    {
        var response = await SimulateOperationToTestCall(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            RelatedAccount = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == LoansErrors.InvalidRelatedAccount.Code);
    }
}
