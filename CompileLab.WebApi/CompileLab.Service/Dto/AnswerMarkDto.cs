using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Dto
{
    public class AnswerMarkDto
    {
        public bool IsSuccess { get; set; }
        public double Mark { get; set; }
        public string? Remark { get; set; }

        public string TypeError { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
