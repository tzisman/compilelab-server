using CheckBox.Repository.Entities;
using CheckBox.Service.Interfaces;
using CheckBox.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckBox.Repository.Interfaces;
using AutoMapper;

namespace CheckBox.Service.Services
{
    public class UserRegisterService(IUserRepository repository, IMapper mapper) : IRegister<UserRegisterDto>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<string> Register(UserRegisterDto item)
        {
            var email = await _repository.GetUserByEmail(item.Email);
            if (email != null)
            {
                throw new InvalidOperationException("The email already exists.");
            }

                User user = _mapper.Map<User>(item);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(item.Password);
            _ = await _repository.AddItem(user);
            return "succses" ;
        }



    }
}
