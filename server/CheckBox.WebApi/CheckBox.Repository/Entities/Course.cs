using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        
        public User Lecturer { get; set; } = null!;

        [ForeignKey("Lecturer")]
        [Required]
        public int LecturerId { get; set; }

        public ICollection<UserInCourse> Studies { get; set; } = new List<UserInCourse>();
    }
}
