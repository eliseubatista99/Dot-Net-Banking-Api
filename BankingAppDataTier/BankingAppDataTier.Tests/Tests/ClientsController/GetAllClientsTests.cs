using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Operations.Clients;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Clients;

public class GetAllClientsTests : OperationTest<GetClientsOperation, VoidOperationInput, GetClientsOutput>
{
    public GetAllClientsTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetClientsOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new VoidOperationInput
        {
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Clients.Count > 0);
    }
}

