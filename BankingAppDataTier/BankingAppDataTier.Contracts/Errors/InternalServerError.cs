using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
