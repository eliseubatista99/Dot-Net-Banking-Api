using BankingAppAuthenticationTier.Library.Database;
using BankingAppAuthenticationTier.Library.Providers;

namespace BankingAppAuthenticationTier.Tests.Mocks
{
    public class DatabaseClientsProviderMock : IDatabaseClientsProvider
    {
        protected List<ClientsTableEntry> clientsMock { get; set; } = new List<ClientsTableEntry>
        {
            new ClientsTableEntry
            {
                Id = "Permanent_Client_01",
                Password = "password",
            },
            new ClientsTableEntry
            {
                Id = "Permanent_Client_02",
                Password = "xe",
            },
            new ClientsTableEntry
            {
                Id = "Permanent_Client_03",
                Password = "password",
            }
        };

        public bool Add(ClientsTableEntry entry)
        {
            return true;
        }

        public bool CreateTableIfNotExists()
        {
            return true;
        }

        public bool Delete(string id)
        {
            return true;
        }

        public bool DeleteAll()
        {
            return true;
        }

        public bool Edit(ClientsTableEntry entry)
        {
            return true;
        }

        public List<ClientsTableEntry> GetAll()
        {
            return clientsMock;
        }

        public ClientsTableEntry? GetById(string id)
        {
            return clientsMock.Find(c => c.Id == id);
        }
    }
}
