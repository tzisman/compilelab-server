using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Entities
{
    public enum ProgrammingLanguage
    {
        CSharp,
        Python
    }

    public class CodeExercise
    {

        [Key]
        public int Id { get; set; }

        public Course Course { get; set; } = null!;

        [ForeignKey("Course")]
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string ExerciseName { get; set; } = string.Empty;

        [Required]
        public ProgrammingLanguage Language { get; set; }

        public string? Description { get; set; }

        public ICollection<TestCase> EdgeCases { get; set; } = new List<TestCase>();
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }
}
