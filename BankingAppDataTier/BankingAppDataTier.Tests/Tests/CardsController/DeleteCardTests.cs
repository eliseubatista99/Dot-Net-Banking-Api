using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Cards;

public class DeleteCardTests : OperationTest<DeleteCardOperation, DeleteCardInput, VoidOperationOutput>
{
    private IDatabaseCardsProvider databaseCardsProvider { get; set; }

    public DeleteCardTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteCardOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseCardsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseCardsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var deleteResponse = await SimulateOperationToTestCall(new DeleteCardInput
        {
            Id = "To_Delete_Debit_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(deleteResponse.Error == null);

        var getByIdResponse = databaseCardsProvider.GetById("To_Delete_Debit_01");

        Assert.True(getByIdResponse == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new DeleteCardInput
        {
            Id = "InvalidId",
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
