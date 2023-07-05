using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Models
{
    public class AppUser : IdentityUser
    {
        
        public Student? student { get; set; }
        public Loging? loging { get; set; }
    }
}
