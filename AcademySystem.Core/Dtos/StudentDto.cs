﻿using AcademySystem.Core.Enums;
using AcademySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Dtos
{
    public class StudentDto
    {
        [Key]
        public int Id { get; set; }
        [Required, DataType(DataType.Text)]
        public string? FirstName { get; set; }
        [Required, DataType(DataType.Text)]
        public string? SecondName { get; set; }
        [Required, DataType(DataType.Text)]
        public string? ThirdName { get; set; }
        [Required, DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public int Identity { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? ImageStudent { get; set; }
        public string? ImageIdentity { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime DateOfEnrollment { get; set; }
        public AcademicLevel Level { get; set; }
        public double Rating { get; set; }
    }
}
