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
    public class StudentAnswerDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserInCourseId { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        public string? AnswerCode { get; set; }

        public double Mark { get; set; }

        public string? Remark { get; set; }
    }
}
