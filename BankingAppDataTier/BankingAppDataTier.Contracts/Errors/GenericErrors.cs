using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Errors
{
    [ExcludeFromCodeCoverage]
    public static class GenericErrors
    {
        public static Error FailedToPerformDatabaseOperation = new Error { Code = "FailedToPerformDatabaseOperation", Message = "Failed to Perform The Database Operation" };

        public static Error InvalidId = new Error { Code = "InvalidId", Message = "No Item Found For The Specified Id" };

        public static Error IdAlreadyInUse = new Error { Code = "IdAlreadyInUse", Message = "Id is already being used" };
    }
}
