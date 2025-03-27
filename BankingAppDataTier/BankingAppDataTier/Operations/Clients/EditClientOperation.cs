using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations.Inputs.Clients;

namespace BankingAppDataTier.Operations.Clients
{
    public class EditClientOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<EditClientInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(EditClientInput input)
        {
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.Surname = input.Surname != null ? input.Surname : entryInDb.Surname;
            entryInDb.BirthDate = input.BirthDate != null ? input.BirthDate.GetValueOrDefault() : entryInDb.BirthDate;
            entryInDb.VATNumber = input.VATNumber != null ? input.VATNumber : entryInDb.VATNumber;
            entryInDb.PhoneNumber = input.PhoneNumber != null ? input.PhoneNumber : entryInDb.PhoneNumber;
            entryInDb.Email = input.Email != null ? input.Email : entryInDb.Email;

            var result = databaseClientsProvider.Edit(entryInDb);

            if (!result)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            return new VoidOperationOutput();
        }
    }
}
