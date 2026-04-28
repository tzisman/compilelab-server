using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Dto
{
    public class CourseReportDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<ExerciseGradeDto> Exercises { get; set; } = new List<ExerciseGradeDto>();
    }

    public class ExerciseGradeDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public double? Grade { get; set; } 
    }
}
