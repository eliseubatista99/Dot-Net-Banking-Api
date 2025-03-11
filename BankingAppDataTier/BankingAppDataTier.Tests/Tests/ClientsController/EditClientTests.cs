﻿using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests;

public class EditClientTests
{
    private ClientsController _clientsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _clientsController = TestMocksBuilder._ClientsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        const string newName = "New Name";
        Setup();

        var result = (ObjectResult)_clientsController.EditClient(new EditClientInput
        {
            Id = "JW0000000",
            Name = newName,
        }).Result!;

        var response = (VoidOutput)result.Value!;

        result = (ObjectResult)_clientsController.GetClientById("JW0000000").Result!;
        var response2 = (GetClientByIdOutput)result.Value!;


        Assert.True(response2.Client?.Name == newName);
    }


    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_clientsController.EditClient(new EditClientInput
        {
            Id = "invalid Id",
            Name = "name",
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
