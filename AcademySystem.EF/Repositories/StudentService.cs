using AcademySystem.Core.Dtos;
using AcademySystem.Core.Enums;
using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AcademySystem.EF.Repositories
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext dBContext;

        public StudentService(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public int CreateStudent(StudentDto studentDto , string appUserId)
        {
            var student = new Student()
            {
                
                BirthDay = studentDto.BirthDay,
                FirstName = studentDto.FirstName,
                SecondName = studentDto.LastName,
                ThirdName = studentDto.ThirdName,
                LastName = studentDto.LastName,
                Identity = studentDto.Identity,
                Level = studentDto.Level,
                Location = studentDto.Location,
                PhoneNumber = studentDto.PhoneNumber,
                Rating = studentDto.Rating,
                DateOfEnrollment = studentDto.DateOfEnrollment,
            };
            student.appUserId = appUserId;
            if (dBContext.Add(student) == null) return -1;
            dBContext.SaveChanges();
            return student.Id;
        }


        public Student? DeleteStudent(int id)
        {
            var student = GetStudent(id);
            if (student == null) return null;
            dBContext.Remove(student);
            dBContext.SaveChanges();
            return student;
        }

        public IQueryable<Student> GetAllStudents(string searchData)
        {
            return dBContext.Students.Where(
                x => string.IsNullOrEmpty(searchData)
                ? true
                : (x.FirstName!.Contains(searchData)
                    || x.PhoneNumber!.Contains(searchData)
                    || nameof(x.Level)!.Contains(searchData)
                )
                
                ) ;
                
        }

        public Student? GetStudent(int id)
        {
            return dBContext.Students.FirstOrDefault(x => x.Id == id);
        }

        public int UpdateStudent(Student? student)
        {
            if(student == null || student.Id <= 0) return -1;
            student = GetStudent(student.Id);
            if (student == null) return -1;
            dBContext.Update(student);
            dBContext.SaveChanges();
            return student.Id;  

        }
    }
}
