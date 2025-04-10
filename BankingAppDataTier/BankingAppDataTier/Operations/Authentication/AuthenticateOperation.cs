using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations;
using AuthenticationTier = BankingAppAuthenticationTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
{
    public class AuthenticateOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AuthenticationTier.AuthenticateInput, AuthenticationTier.AuthenticateOutput>(context, endpoint)
    {
        protected override bool UseAuthentication { get; set; } = false;

        protected override async Task<AuthenticationTier.AuthenticateOutput> ExecuteAsync(AuthenticationTier.AuthenticateInput input)
        {
            return await authTierProvider.Authenticate(input);
        }
    }
}
