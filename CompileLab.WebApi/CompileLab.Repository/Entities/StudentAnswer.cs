using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Entities
{
    public class StudentAnswer
    {

        [Key]
        public int Id { get; set; }

        public UserInCourse? StudentInCourse { get; set; }

        [ForeignKey("StudentInCourse")]
        [Required]
        public int UserInCourseId { get; set; }

        public CodeExercise Exercise { get; set; } = null!;

        [ForeignKey("Exercise")]
        [Required]
        public int ExerciseId { get; set; }

        public string? AnswerCode { get; set; }

        public double Mark { get; set; }

        public string? Remark { get; set; }

    }
}
