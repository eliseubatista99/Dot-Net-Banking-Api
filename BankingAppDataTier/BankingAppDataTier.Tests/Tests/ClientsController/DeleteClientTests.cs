using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Clients;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Clients;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Clients;

public class DeleteClientTests : OperationTest<DeleteClientOperation, DeleteClientInput, VoidOperationOutput>
{
    private IDatabaseClientsProvider databaseClientsProvider { get; set; }

    public DeleteClientTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteClientOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseClientsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var deleteResponse = await SimulateOperationToTestCall(new DeleteClientInput
        {
            Id = "To_Delete_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(deleteResponse.Error == null);

        var getByIdResponse = databaseClientsProvider.GetById("To_Delete_Client_01");

        Assert.True(getByIdResponse == null);
    }


    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new DeleteClientInput
        {
            Id = "Invalidid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_CantCloseWithActiveAccounts()
    {
        var response = await SimulateOperationToTestCall(new DeleteClientInput
        {
            Id = "Permanent_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == ClientsErrors.CantCloseWithActiveAccounts.Code);
    }
}
