
using BankingAppAuthenticationTier.Contracts.Database;

namespace BankingAppAuthenticationTier.Contracts.Providers
{
    public interface IDatabaseTokenProvider
    {
        public bool CreateTableIfNotExists();

        public TokenTableEntry? GetToken(string token);

        public bool Add(TokenTableEntry entry);

        public bool SetExpirationDateTime(string token, DateTime expiration);

        public bool Delete(string token);

        public bool DeleteAllExpired();

        public bool DeleteTokensOfClient(string clientId);

        public bool DeleteAll();
    }
}
