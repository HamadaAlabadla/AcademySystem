using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Models
{
    public class Loging
    {
        [Key]
        public string? appUserId { get; set; }
        public AppUser? appUser { get; set; }
        [Required]
        public bool IsLogging { get; set; } = false;

    }
}
