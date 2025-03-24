﻿using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests.Helpers;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Accounts;

public class DeleteAccountTests : OperationTest<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>
{
    public DeleteAccountTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteAccountOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await TestsHelper.SimulateCall<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>(OperationToTest!, new DeleteAccountInput
        {
            Id = "To_Delete_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidAccountId()
    {
        var response = await TestsHelper.SimulateCall<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>(OperationToTest!, new DeleteAccountInput
        {
            Id = "invalidId",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_CantCloseWithRelatedCards()
    {
        var response = await TestsHelper.SimulateCall<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>(OperationToTest!, new DeleteAccountInput
        {
            Id = "Permanent_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AccountsErrors.CantCloseWithRelatedCards.Code);
    }


    [Fact]
    public async Task ShouldReturnError_CantCloseWithActiveLoans()
    {
        var response = await TestsHelper.SimulateCall<DeleteAccountOperation, DeleteAccountInput, VoidOperationOutput>(OperationToTest!, new DeleteAccountInput
        {
            Id = "Permanent_Current_02",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AccountsErrors.CantCloseWithActiveLoans.Code);
    }
}
