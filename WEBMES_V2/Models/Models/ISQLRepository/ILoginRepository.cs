using WEBMES_V2.Models.DomainModels;
using WEBMES_V2.Models.DTO;

namespace WEBMES_V2.Models.Models.ISQLRepository
{
    public class ILoginRepository
    {
        public interface ILoginRepoConnection
        {
           Task<bool> UserLogin(UserDTO userDTO);
        }
    }
}
