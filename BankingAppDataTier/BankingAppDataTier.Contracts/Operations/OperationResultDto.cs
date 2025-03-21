using BankingAppDataTier.Contracts.Dtos.Entities;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
    using System.Text.Json;
using System.Runtime.Serialization;

namespace BankingAppDataTier.Contracts.Operations
{
    public class OperationResultDto : IResult
    {
        public HttpStatusCode? StatusCode { get; private set; }

        public _BaseOutput? Output { get; private set; }

        public OperationResultDto(HttpStatusCode code)
        {
            this.StatusCode = code;
        }

        public OperationResultDto(_BaseOutput? output) 
        {
            this.StatusCode = output?.StatusCode;
            this.Output = output;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            if(StatusCode == null)
            {
                StatusCode = HttpStatusCode.OK;
            }

            return StatusCode switch
            {
                HttpStatusCode.NotFound => Results.NotFound(Output).ExecuteAsync(httpContext),
                HttpStatusCode.BadRequest => Results.BadRequest(Output).ExecuteAsync(httpContext),
                HttpStatusCode.NoContent => Results.NoContent().ExecuteAsync(httpContext),
                HttpStatusCode.Unauthorized => Results.Unauthorized().ExecuteAsync(httpContext),
                _ => Results.Ok(Output).ExecuteAsync(httpContext),
            };
        }
    }
}
