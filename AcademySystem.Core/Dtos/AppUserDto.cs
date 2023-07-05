using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Dtos
{
    public class AppUserDto
    {
        public string? Id { get; set; }
        public string? UserName { get; set;}
        [Required,DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required, DataType(DataType.Password)]
        public string? PassWord { get; set; }
        [Required, DataType(DataType.Password)]
        public string? ConfirmPassWord { get; set; }
    }
}
