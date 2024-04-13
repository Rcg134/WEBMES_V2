using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WEBMES_V2.Models.Context;
using WEBMES_V2.Models.DomainModels;
using WEBMES_V2.Models.DTO.LoginUserDTO;
using static WEBMES_V2.Models.ISQLRepository.ILoginRepository;

namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class SQLLoginRepository : ILoginRepoConnection
    {
        private readonly CentralAccessContext centralAccessContext;
        private readonly IMapper _mapper;

        public SQLLoginRepository(CentralAccessContext centralAccessContext,
                                  IMapper mapper)
        {
            this.centralAccessContext = centralAccessContext;
            this._mapper = mapper;
        }

        public async Task<UserDTO> UserLogin(UserDTO userDTO)
        {
            var user = await centralAccessContext
                                            .Users
                                            .SingleOrDefaultAsync(usr => usr.Username == userDTO.Username && usr.Password == userDTO.Password);

            if (user != null)
            {
                return _mapper.Map<UserDTO>(user);
            }

            return null;
        }
    }
}
