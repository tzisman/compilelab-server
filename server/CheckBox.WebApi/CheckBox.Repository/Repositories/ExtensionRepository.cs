using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Repositories
{
    public static class ExtensionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<Course>, ItemRepository<Course>>();
            services.AddScoped<IRepository<UserInCourse>, ItemRepository<UserInCourse>>();
            services.AddScoped<IRepository<CodeExercise>, ItemRepository<CodeExercise>>();
            services.AddScoped<IRepository<TestCase>, ItemRepository<TestCase>>();
            services.AddScoped<IRepository<StudentAnswer>, ItemRepository<StudentAnswer>>();
            return services;
        }
    }
}
