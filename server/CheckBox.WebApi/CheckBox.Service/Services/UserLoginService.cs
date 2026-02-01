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
    public class UserLoginService(IUserRepository repository, IMapper mapper) : ILogin<UserLoginDto>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<string> Login(UserLoginDto item)
        {
            var user = await _repository.GetUserByEmail(item.Email);
            if (user != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(item.Password, user.PasswordHash);
                if (isPasswordCorrect)
                {
                    return "succes";
                }
            }
            return "flail";
             
        }
    }
}
