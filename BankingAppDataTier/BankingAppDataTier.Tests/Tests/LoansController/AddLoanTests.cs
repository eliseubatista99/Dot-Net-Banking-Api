using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Loans;

public class AddLoanTests : OperationTest<AddLoanOperation, AddLoanInput, VoidOperationOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public AddLoanTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddLoanOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var addResponse = await SimulateOperationToTestCall(new AddLoanInput
        {
            Loan = new LoanDto
            {
                Id = "Test01",
                Name = "Loan Name",
                LoanType = Contracts.Enums.LoanType.Auto,
                Interest = 7.0M,
                StartDate = new DateTime(2025, 01, 01),
                RelatedOffer = "Permanent_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Duration = 10,
                ContractedAmount = 10,
                PaidAmount = 0,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseLoansProvider.GetById("Test01");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddLoanInput
        {
            Loan = new LoanDto
            {
                Id = "Permanent_AU_01",
                Name = "Loan Name",
                LoanType = Contracts.Enums.LoanType.Auto,
                Interest = 7.0M,
                StartDate = new DateTime(2025, 01, 01),
                RelatedOffer = "Permanent_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Duration = 10,
                ContractedAmount = 10,
                PaidAmount = 0,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}
