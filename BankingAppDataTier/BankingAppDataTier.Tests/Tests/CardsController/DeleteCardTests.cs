using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using ElideusDotNetFramework.Tests.Helpers;

namespace BankingAppDataTier.Tests.Cards;

public class DeleteCardTests : OperationTest<DeleteCardOperation, DeleteCardInput, VoidOperationOutput>
{
    private GetCardByIdOperation getCardByIdOperation;

    public DeleteCardTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteCardOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        getCardByIdOperation = new GetCardByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var deleteResponse = await TestsHelper.SimulateCall<DeleteCardOperation, DeleteCardInput, VoidOperationOutput>(OperationToTest!, new DeleteCardInput
        {
            Id = "To_Delete_Debit_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(deleteResponse.Error == null);

        var getByIdResponse = await TestsHelper.SimulateCall<GetCardByIdOperation, GetCardByIdInput, GetCardByIdOutput>(getCardByIdOperation!, new GetCardByIdInput
        {
            Id = "To_Delete_Debit_01",
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(getByIdResponse.Card == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await TestsHelper.SimulateCall<DeleteCardOperation, DeleteCardInput, VoidOperationOutput>(OperationToTest!, new DeleteCardInput
        {
            Id = "InvalidId",
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
