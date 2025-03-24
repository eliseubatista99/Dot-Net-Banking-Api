﻿using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Operations.Clients;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using ElideusDotNetFramework.Tests.Helpers;

namespace BankingAppDataTier.Tests.Clients;

public class ChangePasswordTests : OperationTest<ChangePasswordOperation, ChangeClientPasswordInput, VoidOperationOutput>
{
    public ChangePasswordTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new ChangePasswordOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newPassword = "NewPassword";

        var response = await SimulateOperationToTestCall(new ChangeClientPasswordInput
        {
            Id = "To_Edit_Client_01",
            PassWord = newPassword,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
    }


    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new ChangeClientPasswordInput
        {
            Id = "invalid Id",
            PassWord = "password",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
