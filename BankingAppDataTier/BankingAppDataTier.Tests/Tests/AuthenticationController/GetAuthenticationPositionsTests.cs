//using BankingAppDataTier.Contracts.Constants;
//using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Authentication;

//public class GetAuthenticationPositionsTests
//{
//    private AuthenticationController _authController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _authController = TestMocksBuilder._AuthenticationControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_authController.GetAuthenticationPositions(new GetAuthenticationPositionsInput
//        {
//            ClientId = "Permanent_Client_01",
//        }).Result!;

//        var response = (GetAuthenticationPositionsOutput)result.Value!;

//        Assert.True(response.Error == null);
//        Assert.True(response.Positions.Count == BankingAppDataTierConstants.DEDFAULT_NUMBER_OF_POSITIONS);
//    }

//    [Theory]
//    [InlineData(1)]
//    [InlineData(4)]
//    [InlineData(10)]
//    public void ShouldBe_Success_ForSpecificNumberOfPositions(int numberOfPositions)
//    {
//        Setup();

//        // This client password has only 8 chars, so we should expect the final count of positions to be 8 at max
//        var finalNumberOfPositions = numberOfPositions < 8 ? numberOfPositions : 8;

//        var result = (ObjectResult)_authController.GetAuthenticationPositions(new GetAuthenticationPositionsInput
//        {
//            ClientId = "Permanent_Client_01",
//            NumberOfPositions = numberOfPositions,
//        }).Result!;

//        var response = (GetAuthenticationPositionsOutput)result.Value!;

//        Assert.True(response.Error == null);
//        Assert.True(response.Positions.Count == finalNumberOfPositions);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidClient()
//    {
//        Setup();

//        var result = (ObjectResult)_authController.GetAuthenticationPositions(new GetAuthenticationPositionsInput
//        {
//            ClientId = "InvalidClient",
//        }).Result!;

//        var response = (GetAuthenticationPositionsOutput)result.Value!;

//        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidClient.Code);
//    }
//}
