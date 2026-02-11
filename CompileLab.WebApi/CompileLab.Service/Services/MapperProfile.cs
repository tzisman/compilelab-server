using AutoMapper;
using CompileLab.Repository.Entities;
using CompileLab.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<UserInCourse, UserInCourseDto>().ReverseMap();
            CreateMap<CodeExercise, CodeExerciseDto>().ReverseMap();
            CreateMap<TestCase, TestCaseDto>().ReverseMap();
            CreateMap<StudentAnswer, StudentAnswerDto>().ReverseMap();
        }
    }
}
