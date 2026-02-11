using AutoMapper;
using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Services
{
    public class UserLoginService(IUserRepository repository, IMapper mapper, IToken<User> token) : ILogin<UserLoginDto>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IToken<User> _tokenService = token;
        public async Task<string> Login(UserLoginDto item)
        {
            var user = await _repository.GetUserByEmail(item.Email);
            if (user != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(item.Password, user.PasswordHash);
                if (isPasswordCorrect)
                {
                    var jwt = _tokenService.CreateToken(user);
                    return jwt;
                }
            }
            throw new UnauthorizedAccessException("email or passwors is incorrect");

        }
    }
}

