﻿using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Loans;

public class GetLoansOfClientTests : OperationTest<GetLoansOfClientOperation, GetLoansOfClientInput, GetLoansOfClientOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public GetLoansOfClientTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetLoansOfClientOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }


    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new GetLoansOfClientInput
        {
            ClientId = "Permanent_Client_01",
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
        var response = await SimulateOperationToTestCall(new GetLoansOfClientInput
        {
            ClientId = "Permanent_Client_01",
            LoanType = loanType,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Loans.Count > 0);
        Assert.True(!response.Loans.Exists(l => l.LoanType != loanType));
    }
}
