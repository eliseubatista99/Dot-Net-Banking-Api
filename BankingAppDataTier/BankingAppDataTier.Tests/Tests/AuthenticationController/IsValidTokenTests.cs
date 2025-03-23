//using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Constants;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Authentication;

//public class IsValidTokenTests
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

//        var result = (ObjectResult)_authController.IsValidToken(new IsValidTokenInput
//        {
//            Token = TestsConstants.PermanentToken,
//        }).Result!;

//        var response = (IsValidTokenOutput)result.Value!;

//        Assert.True(response.Error == null);
//        Assert.True(response.IsValid);
//        Assert.True(response.ExpirationDateTime != null);
//    }

//    [Fact]
//    public void ShouldBe_InvalidForExpiredToken()
//    {
//        Setup();

//        var result = (ObjectResult)_authController.IsValidToken(new IsValidTokenInput
//        {
//            Token = TestsConstants.ExpiredToken,
//        }).Result!;

//        var response = (IsValidTokenOutput)result.Value!;

//        Assert.True(response.Error == null);
//        Assert.True(!response.IsValid);
//        Assert.True(response.Reason == AuthenticationErrors.TokenExpired.Code);
//    }

//    [Fact]
//    public void ShouldBe_InvalidForInvalidToken()
//    {
//        Setup();

//        var result = (ObjectResult)_authController.IsValidToken(new IsValidTokenInput
//        {
//            Token = "invalid-token",
//        }).Result!;

//        var response = (IsValidTokenOutput)result.Value!;

//        Assert.True(response.Error == null);
//        Assert.True(!response.IsValid);
//        Assert.True(response.Reason == AuthenticationErrors.InvalidToken.Code);
//    }
//}
