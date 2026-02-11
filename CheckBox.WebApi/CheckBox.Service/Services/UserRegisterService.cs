using AutoMapper;
using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Services
{
    public class UserRegisterService(IUserRepository repository, IMapper mapper, IToken<User> token) : IRegister<UserRegisterDto>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IToken<User> _tokenService = token;

        public async Task<string> Register(UserRegisterDto item)
        {
            var email = await _repository.GetUserByEmail(item.Email);
            if (email != null)
            {
                throw new InvalidOperationException("The email already exists.");
            }

            User user = _mapper.Map<User>(item);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(item.Password);
            User newUser = await _repository.AddItem(user);
            var jwt = _tokenService.CreateToken(newUser);
            return jwt;
        }



    }
}
