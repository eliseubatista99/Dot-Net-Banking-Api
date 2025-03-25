﻿using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
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

public class DeleteLoanTests : OperationTest<DeleteLoanOperation, DeleteLoanInput, VoidOperationOutput>
{
    private IDatabaseLoansProvider databaseLoansProvider { get; set; }

    public DeleteLoanTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteLoanOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoansProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var delResponse = await SimulateOperationToTestCall(new DeleteLoanInput
        {
            Id = "To_Delete_AU_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(delResponse.Error == null);

        var getByIdResponse = databaseLoansProvider.GetById("To_Delete_AU_01");

        Assert.True(getByIdResponse == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new DeleteLoanInput
        {
            Id = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
