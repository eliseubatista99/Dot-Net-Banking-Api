﻿using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Loans;

public class GetLoanByIdTests
{
    private LoansController _loansController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _loansController = TestMocksBuilder._LoansControllerMock;
    }

    [Theory]
    [InlineData("Permanent_AU_01")]
    [InlineData("Permanent_MO_01")]
    [InlineData("Permanent_PE_01")]
    public void ShouldBe_Success(string id)
    {
        Setup();

        var result = (ObjectResult)_loansController.GetLoanById(id).Result!;

        var response = (GetLoanByIdOutput)result.Value!;

        Assert.True(response.Loan != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_loansController.GetLoanById("invalid").Result!;

        var response = (GetLoanByIdOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

}
