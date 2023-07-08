using AcademySystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int Identity { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Level { get; set; }
    }
}
