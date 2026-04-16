using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Dto
{
    public class CourseDisplayDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int StudiesCount { get; set; }

        public int ExercisesCount { get; set; }

    }
}
