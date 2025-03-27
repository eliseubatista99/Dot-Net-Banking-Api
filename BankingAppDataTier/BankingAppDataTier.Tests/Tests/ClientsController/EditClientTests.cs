using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Clients;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Clients;

public class EditClientTests : OperationTest<EditClientOperation, EditClientInput, VoidOperationOutput>
{
    private IDatabaseClientsProvider databaseClientsProvider { get; set; }

    public EditClientTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditClientOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseClientsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newName = "New Name";

        var editResponse = await SimulateOperationToTestCall(new EditClientInput
        {
            Id = "To_Edit_Client_01",
            Name = newName,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = databaseClientsProvider.GetById("To_Edit_Client_01");

        Assert.True(getByIdResponse?.Name == newName);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new EditClientInput
        {
            Id = "invalid Id",
            Name = "name",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}
