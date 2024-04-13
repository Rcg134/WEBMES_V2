using Microsoft.Data.SqlClient;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IDapperConnection
    {
        SqlConnection CreateConnection();
    }
}
