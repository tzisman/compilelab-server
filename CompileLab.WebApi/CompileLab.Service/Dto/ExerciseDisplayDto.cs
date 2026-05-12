using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Dto
{
    public class ExerciseDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProgrammingLanguage { get; set; } = string.Empty;
        public double? Grade { get; set; } 

        public int? StudentAnswerId {get; set; }
    }
}
