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
    public class TestCaseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        [Required]
        public string Input { get; set; }

        [Required]
        public string Output { get; set; } 
    }
}
