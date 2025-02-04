using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name = "GetClients")]
        public List<ClientDto> Get()
        {
            List<ClientDto> result = new List<ClientDto>();
            SqlConnection sqlConnection = new SqlConnection("");
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.Parameters.Clear();

            sqlCommand.CommandText = $"SELECT * FROM {ClientsTable.TABLE_NAME}";

            sqlConnection.Open();
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();

            while (sqlReader.Read())
            {
                var dataEntry = ClientsMapperProfile.MapSqlDataToClientTableEntry(sqlReader);

                result.Add(ClientsMapperProfile.MapClientTableEntryToClientDto(dataEntry));
            }

            sqlConnection.Close();

            return result;
        }
    }
}
