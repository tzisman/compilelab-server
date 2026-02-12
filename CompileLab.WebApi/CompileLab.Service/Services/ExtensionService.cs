using CompileLab.DataContext;
using CompileLab.Repository.Entities;
using CompileLab.Repository.Repositories;
using CompileLab.Service.Authorization;
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
            services.AddScoped<IUserInCourseAuthorization, UserInCourseAuthorization>();
            services.AddScoped<IAnswerAuthorization, AnswerAuthorization>();
            services.AddScoped<IService<StudentAnswerDto>, StudentAnswerService>();
            services.AddKeyedScoped<IAuthorization, CourseAuthorization>("course");
            services.AddKeyedScoped<IAuthorization, ExerciseAuthorization>("exercise");
            services.AddKeyedScoped<IAuthorization, TestCaseAuthorization>("testCase");
            services.AddScoped<IToken<User>, TokenService>();
            return services;
        }
    }
}
