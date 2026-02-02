using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Dto
{
    public class UserInCourseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
