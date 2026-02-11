using CheckBox.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Dto
{
    public class CodeExerciseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public ProgrammingLanguage Language { get; set; }

        public string? Description { get; set; }

    }
}
