using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Clients;
using BankingAppDataTier.Operations.LoanOffers;
using BankingAppDataTier.Operations.Loans;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using Microsoft.AspNetCore.Mvc;

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
