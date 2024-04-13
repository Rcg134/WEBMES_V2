using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WEBMES_V2.Models.ISQLRepository;

namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class DapperConnectionRepository : IDapperConnection
    {
        private readonly IConfiguration _configuration;

        public DapperConnectionRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(
                           _configuration.GetConnectionString("MES_ATEC_Connection"));
        }
    }
}
