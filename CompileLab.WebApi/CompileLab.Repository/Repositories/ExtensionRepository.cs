using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Repositories
{
    public static class ExtensionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<UserInCourse>, UserInCourseRepository>();
            services.AddScoped<IRepository<CodeExercise>, CodeExerciseRepository>();
            services.AddScoped<IRepository<TestCase>, TestCaseRepository>();
            services.AddScoped<IRepository<StudentAnswer>, StudentAnswerRepository>();
            return services;
        }
    }
}
