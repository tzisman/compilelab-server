using AutoMapper;
using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class CodeExerciseService(IRepository<CodeExercise> repository, IMapper mapper) : IService<CodeExerciseDto>
    {
        private readonly IRepository<CodeExercise> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<CodeExerciseDto> AddItem(CodeExerciseDto item)
        {
            var codeExercise = _mapper.Map<CodeExercise>(item);
            var result = await _repository.AddItem(codeExercise);
            var newCodeExercise = _mapper.Map<CodeExerciseDto>(result);
            return newCodeExercise;
        }

        public Task<CodeExerciseDto> AddItem(CodeExerciseDto item, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public Task DeleteItem(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CodeExerciseDto>> GetAll()
        {
            var result = await _repository.GetAll();
            var resultDto = _mapper.Map<List<CodeExerciseDto>>(result);
            return resultDto;
        }

        public async Task<CodeExerciseDto> GetById(int id)
        {
            var result = await _repository.GetById(id);
            var resultDto = _mapper.Map<CodeExerciseDto>(result);
            return resultDto;
        }

        public  async Task<CodeExerciseDto> UpdateItem(int id, CodeExerciseDto item)
        {
            var codeExercise = _mapper.Map<CodeExercise>(item);
            var result = await _repository.UpdateItem(id, codeExercise);
            var resultDto = _mapper.Map<CodeExerciseDto>(result);
            return resultDto;
        }

        public Task<CodeExerciseDto> UpdateItem(int id, CodeExerciseDto item, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
