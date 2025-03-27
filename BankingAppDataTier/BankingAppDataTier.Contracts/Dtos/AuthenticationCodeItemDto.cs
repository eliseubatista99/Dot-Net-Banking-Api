using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos
{
    [ExcludeFromCodeCoverage]

    public class AuthenticationCodeItemDto
    {
        public required int Position { get; set; }

        public required char Value { get; set; }
    }
}
