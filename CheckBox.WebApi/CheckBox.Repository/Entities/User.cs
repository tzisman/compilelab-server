using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;


        public ICollection<UserInCourse> Studies { get; set; } = new List<UserInCourse>();
        public ICollection<Course> Lectures { get; set; } = new List<Course>();
    }
}
