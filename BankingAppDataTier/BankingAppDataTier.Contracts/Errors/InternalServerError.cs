using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BankingAppDataTier.Contracts.Errors
{
    public class InternalServerError : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        public InternalServerError()
            : base(DefaultStatusCode)
        {
        }

        public InternalServerError([ActionResultObjectValue] object? error)
            : base(error)
        {
            StatusCode = DefaultStatusCode;
        }
    }
}
