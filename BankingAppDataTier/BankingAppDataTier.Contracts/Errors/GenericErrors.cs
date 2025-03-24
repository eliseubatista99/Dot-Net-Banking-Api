using ElideusDotNetFramework.Errors.Contracts;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class GenericErrors
    {
        public static Error Unauthorized = new Error { Code = "Unauthorized", Message = "Unauthorized" };

        public static Error InternalServerError = new Error { Code = "InternalServerError", Message = "Something Went Wrong" };

        public static Error FailedToPerformDatabaseOperation = new Error { Code = "FailedToPerformDatabaseOperation", Message = "Failed to Perform The Database Operation" };

        public static Error InvalidId = new Error { Code = "InvalidId", Message = "No Item Found For The Specified Id" };

        public static Error IdAlreadyInUse = new Error { Code = "IdAlreadyInUse", Message = "Id is already being used" };

        public static Error InvalidInputError(string field = "")
        {
            var message = "Invalid Input";

            if(field != string.Empty)
            {
                message += $": {field}";
            }
            return new Error { Code = "InvalidInput", Message = message };
        }
    }
}
