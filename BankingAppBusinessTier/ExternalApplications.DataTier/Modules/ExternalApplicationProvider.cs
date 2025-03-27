using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;

namespace ExternalApplications.DataTier.Modules
{
    public class ExternalApplicationProvider
    {
        protected IApplicationContext applicationContext { get; set; }

        protected string externalServiceUrl = "";

        protected static HttpClient httpClient;

        public ExternalApplicationProvider(IApplicationContext _applicationContext) 
        {
            this.applicationContext = _applicationContext;
        }

        protected virtual async Task<TOut> CallExternalPostOperation<TIn, TOut>(string endpoint, TIn input) where TIn : OperationInput where TOut : OperationOutput
        {
            httpClient.BaseAddress = new Uri(externalServiceUrl);
            httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(JsonSerializer.Serialize(input),
                                                Encoding.UTF8,
                                                "application/json");//CONTENT-TYPE header



            var response = await httpClient.SendAsync(request);

            //response.EnsureSuccessStatusCode();

            var tau = await response.Content.ReadAsStringAsync();
            var operationHttpResult = await response.Content.ReadFromJsonAsync<TOut>();

            return operationHttpResult!;
        }
    }
}
