using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    [ExcludeFromCodeCoverage]

    public class AuthenticationCodeDto
    {
        public required List<AuthenticationCodeItemDto> Code { get; set; }
    }
}
