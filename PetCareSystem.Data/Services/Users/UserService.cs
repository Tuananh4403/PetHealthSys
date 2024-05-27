using AutoMapper;
using BCrypt.Net;

using Microsoft.AspNetCore.Identity.Data;
using PetCareSystem.Data.Entites;
using PetCareSystem.WebApp.Models;
using PetCareSystem.Data.EF;
using PetHealthSys.PetCareSystem.WebApp.Models;
using PetHealthSys.PetCareSystem.WebApp.Authorization;
using PetHealthSys.PetCareSystem.WebApp.Helpers;

namespace PetHealthSys.PetCareSystem.Data.Services.Users
{
    public class UserService : IUserService
    {
        private PetHealthDBContext _dbContext;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(PetHealthDBContext dbContext, IJwtUtils jwtUtils, IMapper mapper)
        {
            _dbContext = dbContext;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == model.Email);

            //validate
            if(user == null || BCrypt.Net.BCrypt.Verify(model.Password, user.Password)) {
                throw new AppException("Username or password is incorrect");
            }

            //success
            var res = _mapper.Map<AuthenticateResponse>(user);
            res.Token = _jwtUtils.GenerateToken(user);
            return res;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
