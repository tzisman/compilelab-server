using CompileLab.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Interfaces
{
    public interface IContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry Entry(object entity);
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserInCourse> UserInCourses { get; set; }
        public DbSet<CodeExercise> CodeExercises { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<TestCase> TestCases { get; set; }

        public Task Save();
    }
}
