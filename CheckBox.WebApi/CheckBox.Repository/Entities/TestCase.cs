using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Entities
{
    public class TestCase
    {
        [Key]
        public int Id { get; set; }

        public CodeExercise Exercise { get; set; } = null!;

        [ForeignKey("Exercise")]
        [Required]
        public int ExerciseId { get; set; }

        [Required]
        public string Input { get; set; } = string.Empty;

        [Required]
        public string Output { get; set; } = string.Empty;
    }
}
