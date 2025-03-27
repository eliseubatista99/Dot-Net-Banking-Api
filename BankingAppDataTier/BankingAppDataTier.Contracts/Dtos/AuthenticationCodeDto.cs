using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos
{
    [ExcludeFromCodeCoverage]

    public class AuthenticationCodeDto
    {
        public required List<AuthenticationCodeItemDto> Code { get; set; }
    }
}
