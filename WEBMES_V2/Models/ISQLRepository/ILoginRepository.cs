using WEBMES_V2.Models.DomainModels;
using WEBMES_V2.Models.DTO.LoginUserDTO;

namespace WEBMES_V2.Models.ISQLRepository
{
    public class ILoginRepository
    {
        public interface ILoginRepoConnection
        {
            Task<UserDTO> UserLogin(UserDTO userDTO);
        }
    }
}
