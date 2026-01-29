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
    public class UserRegister(IRepository<User> repository, IMapper mapper) : IRegister<UserRegisterDto>
    {
        private readonly IRepository<User> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<string> Register(UserRegisterDto item)
        {
            User user = _mapper.Map<User>(item);
            _ = await _repository.AddItem(user);
            return "succses" ;
        }



    }
}
