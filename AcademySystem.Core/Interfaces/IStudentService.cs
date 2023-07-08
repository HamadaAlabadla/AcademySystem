using AcademySystem.Core.Dtos;
using AcademySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Interfaces
{
    public interface IStudentService
    {
        Student? GetStudent(int id);

        IQueryable<Student> GetAllStudents(string searchData);
        int CreateStudent(StudentDto studentDto , string appUserId);
        int UpdateStudent(Student? student);
        Student? DeleteStudent(int id);

    }
}
