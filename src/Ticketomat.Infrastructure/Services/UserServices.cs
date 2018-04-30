using System;
using System.Threading.Tasks;
using AutoMapper;
using Ticketomat.Core.Domain;
using Ticketomat.Core.Repositories;
using Ticketomat.Infrastructure.DTO;
using Ticketomat.Infrastructure.Extensions;

namespace Ticketomat.Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mappper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mappper;
        }


        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception("User exist");
            }
            user = new User(userId, role, name, email, password);
            await _userRepository.AddAsync(user);
        }


        public async Task<TokenDTO> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if(user == null)
            {
                throw new Exception("Invalid credential.");
            }
            if(user.Password != password)
            {
                throw new Exception("Invalid credential.");
            }

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDTO
            {
                Token = jwt.Token,
                Expires =  jwt.Expires,
                role = user.Role
            };

        }

        public async Task<AccountDTO> GetAcountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            return _mapper.Map<AccountDTO>(user);
        }
    }
}