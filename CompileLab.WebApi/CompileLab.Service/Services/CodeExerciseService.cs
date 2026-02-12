using AutoMapper;
using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class CodeExerciseService(IRepository<CodeExercise> repository, IMapper mapper,
        [FromKeyedServices("course")] IAuthorization courseAuth,
        [FromKeyedServices("exercise")] IAuthorization exerciseAuth
        ) : IService<CodeExerciseDto>
    {
        private readonly IRepository<CodeExercise> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorization _courseAuth = courseAuth;
        private readonly IAuthorization _exerciseAuth = exerciseAuth;


        public async Task<CodeExerciseDto> AddItem(CodeExerciseDto item, int userId)
        {
            if (! await _courseAuth.IsOwnerOf(item.CourseId, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to add a course.");
            }

            var codeExercise = _mapper.Map<CodeExercise>(item);
            var result = await _repository.AddItem(codeExercise);
            var newCodeExercise = _mapper.Map<CodeExerciseDto>(result);
            return newCodeExercise;
        }


        public async Task DeleteItem(int id, int userId)
        {
            if (!await _exerciseAuth.IsOwnerOf(id, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            await _repository.DeleteItem(id);
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


        public async Task<CodeExerciseDto> UpdateItem(int id, CodeExerciseDto item, int userId)
        {
            if (!await _exerciseAuth.IsOwnerOf(id, userId) || !await _courseAuth.IsOwnerOf(item.CourseId, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to update a course.");
            }
            var codeExercise = _mapper.Map<CodeExercise>(item);
            var result = await _repository.UpdateItem(id, codeExercise);
            var resultDto = _mapper.Map<CodeExerciseDto>(result);
            return resultDto;
        }
    }
}
