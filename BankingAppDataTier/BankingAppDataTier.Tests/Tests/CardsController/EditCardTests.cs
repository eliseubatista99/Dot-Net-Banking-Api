using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using ElideusDotNetFramework.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class EditCardTests : OperationTest<EditCardOperation, EditCardInput, VoidOperationOutput>
{
    private GetCardByIdOperation getCardByIdOperation;

    public EditCardTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditCardOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        getCardByIdOperation = new GetCardByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success_DebitCard()
    {
        const string newName = "MyCardNameTest";

        var editResponse = await TestsHelper.SimulateCall<EditCardOperation, EditCardInput, VoidOperationOutput>(OperationToTest!, new EditCardInput
        {
            Id = "To_Edit_Debit_01",
            Name = newName,
            RequestDate = new DateTime(2025, 02, 01),
            ActivationDate = new DateTime(2025, 02, 15),
            ExpirationDate = new DateTime(2028, 02, 15),
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = await TestsHelper.SimulateCall<GetCardByIdOperation, GetCardByIdInput, GetCardByIdOutput>(getCardByIdOperation!, new GetCardByIdInput
        {
            Id = "To_Edit_Debit_01",
            Metadata = TestsConstants.TestsMetadata,
        });
        
        Assert.True(getByIdResponse.Card?.Name == newName);
    }

    [Fact]
    public async Task ShouldBe_Success_CreditCard()
    {
        const string newName = "MyCardNameTest";

        var editResponse = await TestsHelper.SimulateCall<EditCardOperation, EditCardInput, VoidOperationOutput>(OperationToTest!, new EditCardInput
        {
            Id = "To_Edit_Credit_01",
            Name = newName,
            RequestDate = new DateTime(2025, 02, 01),
            ActivationDate = new DateTime(2025, 02, 15),
            ExpirationDate = new DateTime(2028, 02, 15),
            PaymentDay = 11,
            Balance = 50,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = await TestsHelper.SimulateCall<GetCardByIdOperation, GetCardByIdInput, GetCardByIdOutput>(getCardByIdOperation!, new GetCardByIdInput
        {
            Id = "To_Edit_Credit_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Card?.Name == newName);
    }

    [Fact]
    public async Task ShouldBe_Success_PrePaidCard()
    {
        const string newName = "MyCardNameTest";

        var editResponse = await TestsHelper.SimulateCall<EditCardOperation, EditCardInput, VoidOperationOutput>(OperationToTest!, new EditCardInput
        {
            Id = "To_Edit_PrePaid_01",
            Name = newName,
            RequestDate = new DateTime(2025, 02, 01),
            ActivationDate = new DateTime(2025, 02, 15),
            ExpirationDate = new DateTime(2028, 02, 15),
            Balance = 50,
            Metadata = TestsConstants.TestsMetadata
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = await TestsHelper.SimulateCall<GetCardByIdOperation, GetCardByIdInput, GetCardByIdOutput>(getCardByIdOperation!, new GetCardByIdInput
        {
            Id = "To_Edit_PrePaid_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Card?.Name == newName);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await TestsHelper.SimulateCall<EditCardOperation, EditCardInput, VoidOperationOutput>(OperationToTest!, new EditCardInput
        {
            Id = "invalid_id",
            Name = "test",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
