using CheckBox.DataContext;
using CheckBox.Repository.Entities;
using CheckBox.Repository.Repositories;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Services
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddDataLayer(connectionString);
            services.AddRepository();
            services.AddScoped<IRegister<UserRegisterDto>, UserRegisterService>();
            services.AddScoped<ILogin<UserLoginDto>, UserLoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IService<CourseDto>, CourseService>();
            services.AddScoped<IService<UserInCourseDto>, UserInCourseService>();
            services.AddScoped<IService<CodeExerciseDto>, CodeExerciseService>();
            services.AddScoped<IService<TestCaseDto>, TestCaseService>();
            services.AddScoped<IService<StudentAnswerDto>, StudentAnswerService>();
            services.AddScoped<IToken<User>, TokenService>();
            return services;
        }
    }
}
