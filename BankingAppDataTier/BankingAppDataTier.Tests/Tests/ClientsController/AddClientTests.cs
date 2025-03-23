//using BankingAppDataTier.Contracts.Dtos.Entitites;
//using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Clients;

//public class AddClientTests
//{
//    private ClientsController _clientsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _clientsController = TestMocksBuilder._ClientsControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_clientsController.AddClient(new AddClientInput
//        {
//            Client = new ClientDto
//            {
//                Id = "T0001",
//                Name = "Test",
//                Surname = "Client",
//                BirthDate = new DateTime(1990, 02, 13),
//                VATNumber = "123123123",
//                PhoneNumber = "911111111",
//                Email = "test.client@dotnetbanking.com"
//            },
//            PassWord = "password"
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);
//    }


//    [Fact]
//    public void ShouldReturnError_IdAlreadyInUse()
//    {
//        Setup();

//        var result = (ObjectResult)_clientsController.AddClient(new AddClientInput
//        {
//            Client = new ClientDto
//            {
//                Id = "Permanent_Client_01",
//                Name = "John",
//                Surname = "Wick",
//                BirthDate = new DateTime(1990, 02, 13),
//                VATNumber = "123123123",
//                PhoneNumber = "911111111",
//                Email = "jonh.wick@dotnetbanking.com"
//            },
//            PassWord = "password"
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
//    }
//}
