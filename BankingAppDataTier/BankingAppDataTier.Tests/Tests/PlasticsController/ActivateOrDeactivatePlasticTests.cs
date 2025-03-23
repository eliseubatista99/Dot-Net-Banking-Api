using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.Plastics;

public class ActivateOrDeactivatePlasticTests
{
    private PlasticsController _plasticsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _plasticsController = TestMocksBuilder._PlasticsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_plasticsController.ActivateOrDeactivatePlastic(new ActivateOrDeactivatePlasticInput
        {
            Id = "Permanent_Debit_01",
            Active = false,
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_plasticsController.GetPlasticsOfType(Contracts.Enums.CardType.Debit).Result!;

        var response2 = (GetPlasticsOfTypeOutput)result.Value!;

        Assert.True(response2.Plastics.Find(l => l.Id == "Permanent_Debit_01") == null);

        result = (ObjectResult)_plasticsController.ActivateOrDeactivatePlastic(new ActivateOrDeactivatePlasticInput
        {
            Id = "Permanent_Debit_01",
            Active = true,
        }).Result!;

        response = (VoidOperationOutput)result.Value!;
        Assert.True(response.Error == null);

        result = (ObjectResult)_plasticsController.GetPlasticsOfType(Contracts.Enums.CardType.Debit).Result!;
        response2 = (GetPlasticsOfTypeOutput)result.Value!;

        Assert.True(response2.Plastics.Find(l => l.Id == "Permanent_Debit_01") != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_plasticsController.ActivateOrDeactivatePlastic(new ActivateOrDeactivatePlasticInput
        {
            Id = "invalidId",
            Active = false,
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
