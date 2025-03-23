//using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Constants;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Authentication;

//public class KeepAliveTests
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


//        var isValidResult = (ObjectResult)_authController.IsValidToken(new IsValidTokenInput
//        {
//            Token = TestsConstants.PermanentToken,
//        }).Result!;

//        var isValidResponse = (IsValidTokenOutput)isValidResult.Value!;

//        var originalExpirationTime = isValidResponse.ExpirationDateTime.GetValueOrDefault();

//        var result = (ObjectResult)_authController.KeepAlive(new KeepAliveInput
//        {
//            Token = TestsConstants.PermanentToken,
//        }).Result!;

//        var response = (KeepAliveOutput)result.Value!;

//        Assert.True(response.Error == null);
//        Assert.True(response.ExpirationDateTime.GetValueOrDefault().Ticks > originalExpirationTime.Ticks);
//    }

//    [Fact]
//    public void ShouldBeError_InvalidToken()
//    {
//        Setup();

//        var result = (ObjectResult)_authController.KeepAlive(new KeepAliveInput
//        {
//            Token = "invalid-token",
//        }).Result!;

//        var response = (KeepAliveOutput)result.Value!;

//        Assert.True(response.Error.Code == AuthenticationErrors.InvalidToken.Code);
//    }

//    [Fact]
//    public void ShouldBeError_TokenExpired()
//    {
//        Setup();

//        var result = (ObjectResult)_authController.KeepAlive(new KeepAliveInput
//        {
//            Token = TestsConstants.ExpiredToken,
//        }).Result!;

//        var response = (KeepAliveOutput)result.Value!;

//        Assert.True(response.Error.Code == AuthenticationErrors.TokenExpired.Code);
//    }
//}
