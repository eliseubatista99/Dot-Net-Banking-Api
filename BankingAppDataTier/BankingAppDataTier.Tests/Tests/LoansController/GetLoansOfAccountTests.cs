using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Loans;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Loans;

public class GetLoansOfAccountTests : OperationTest<GetLoansOfAccountOperation, GetLoansOfAccountInput, GetLoansOfAccountOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public GetLoansOfAccountTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetLoansOfAccountOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new GetLoansOfAccountInput
        {
            AccountId = "Permanent_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(response.Loans.Count > 0);
    }

    [Theory]
    [InlineData(LoanType.Personal)]
    [InlineData(LoanType.Auto)]
    [InlineData(LoanType.Mortgage)]
    public async Task ShouldBe_Success_For_LoanType(LoanType loanType)
    {
        var response = await SimulateOperationToTestCall(new GetLoansOfAccountInput
        {
            AccountId = "Permanent_Current_01",
            LoanType = loanType,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Loans.Count > 0);
        Assert.True(!response.Loans.Exists(l => l.LoanType != loanType));
    }
}
