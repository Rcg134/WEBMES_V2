using Microsoft.EntityFrameworkCore;
using WEBMES_V2.Models.Context;
using WEBMES_V2.Models.DomainModels;
using WEBMES_V2.Models.DTO;
using WEBMES_V2.Models.Models.ISQLRepository;
using static WEBMES_V2.Models.Models.ISQLRepository.ILoginRepository;

namespace WEBMES_V2.Models.Models.SQLRepositoryImplementation
{
    public class SQLLoginRepository : ILoginRepoConnection
    {
        private readonly CentralAccessContext centralAccessContext;

        public SQLLoginRepository(CentralAccessContext centralAccessContext)
        {
            this.centralAccessContext = centralAccessContext;
        }

        public async Task<bool> UserLogin(UserDTO userDTO)
        {
            return await centralAccessContext.Users.AnyAsync(usr => usr.Username == userDTO.Username && 
                                                                    usr.Password == userDTO.Password);
        }
    }
}
