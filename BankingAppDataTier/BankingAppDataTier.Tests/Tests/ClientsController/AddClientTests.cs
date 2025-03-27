using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Clients;

public class AddClientTests : OperationTest<AddClientOperation, AddClientInput, VoidOperationOutput>
{
    private IDatabaseClientsProvider databaseClientsProvider { get; set; }

    public AddClientTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddClientOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseClientsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new AddClientInput
        {
            Client = new ClientDto
            {
                Id = "T0001",
                Name = "Test",
                Surname = "Client",
                BirthDate = new DateTime(1990, 02, 13),
                VATNumber = "123123123",
                PhoneNumber = "911111111",
                Email = "test.client@dotnetbanking.com"
            },
            PassWord = "password",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);


        var getByIdResponse = databaseClientsProvider.GetById("T0001");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddClientInput
        {
            Client = new ClientDto
            {
                Id = "Permanent_Client_01",
                Name = "John",
                Surname = "Wick",
                BirthDate = new DateTime(1990, 02, 13),
                VATNumber = "123123123",
                PhoneNumber = "911111111",
                Email = "jonh.wick@dotnetbanking.com"
            },
            PassWord = "password",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}
