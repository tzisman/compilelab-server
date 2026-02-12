using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Authorization
{
    public class UserInCourseAuthorization(IUserInCourseRepository repository,
        IRepository<CodeExercise> exerciseRepo) : IUserInCourseAuthorization
    {
        private readonly IUserInCourseRepository _repository = repository;
        private readonly IRepository<CodeExercise> _exerciseRepo = exerciseRepo;

        public async Task<bool> IsAllowedToChange(int userInCourseId)
        {
            var result = await _repository.GetById(userInCourseId);
            if(result == null)
                return false;
            return result.Status == CourseStatus.Approved;
        }

        public async Task<bool> IsInCourse(int exerciseId, int userInCourseId)
        {
            var uic = await _repository.GetById(userInCourseId);
            var exercise = await _exerciseRepo.GetById(exerciseId);
            if(uic == null || exercise == null)
                return false;
            return uic.CourseId == exercise.CourseId;
        }

        public async Task<bool> IsOwnerOf(int targetId, int userId)
        {
            var result = await _repository.GetById(targetId);
            if (result == null)
                return false;
            
            return result.UserId == userId;
        }
    }
}
