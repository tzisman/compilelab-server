using CheckBox.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CheckBox.DataContext
{
    public class CheckBoxContext: DbContext
    {
        public CheckBoxContext(DbContextOptions<CheckBoxContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserInCourse> UsersInCourses { get; set; }
        public DbSet<CodeExercise> CodeExercises { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<TestCase> TestCases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Lecturer)
                .WithMany(u => u.Lectures)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserInCourse>()
                .Property(uic => uic.Status)
                .HasConversion<string>();

            modelBuilder.Entity<CodeExercise>()
                .Property(ce => ce.Language)
                .HasConversion<string>();


            modelBuilder.Entity<UserInCourse>()
                .HasIndex(uic => new { uic.UserId, uic.CourseId })
                .IsUnique();

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.StudentInCourse)
                .WithMany(uic => uic.Answers)
                .HasForeignKey(sa => sa.UserInCourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TestCase>()
                .HasOne(tc => tc.Exercise)
                .WithMany(e => e.EdgeCases)
                .HasForeignKey(tc => tc.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
