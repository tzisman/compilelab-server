using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Entities
{
        public enum CourseStatus
        {
            Sent,
            Approved,
            Rejected
        }
        public class UserInCourse
        {
            [Key]
            public int Id { get; set; }

            public User? Student { get; set; }

            [ForeignKey("Student")]
            [Required]
            public int UserId { get; set; }


            public Course? Course { get; set; }

            [ForeignKey("Course")]
            [Required]
            public int CourseId { get; set; }

            public CourseStatus Status { get; set; }

            public ICollection<StudentAnswer> Answers { get; set; } = new List<StudentAnswer>();
        }
}
