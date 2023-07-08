using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using Microsoft.AspNetCore.Identity;
using AcademySystem.Core.Enums;
using AcademySystem.Core.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using AcademySystem.EF.Repositories;
using AcademySystem.Core.ViewModel;
using System.Linq.Dynamic.Core;
using Hangfire;

namespace Students.Web.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAppUserService appUserService;
        private readonly IStudentService studentService;
        public StudentsController(
            IMapper mapper,
            IStudentService studentService,
            IAppUserService appUserService)
        {
            this.mapper = mapper;
            this.appUserService = appUserService;
            this.studentService = studentService;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            //BackgroundJob.Enqueue(() => sendMassage("user@example.com"));

            //Console.WriteLine(DateTime.Now);
            //BackgroundJob.Schedule(() => sendMassage("user@example.com") , TimeSpan.FromSeconds(15));

            RecurringJob.AddOrUpdate(() => sendMassage("Email@email.com"), Cron.Minutely());
            return View();
        }
        [HttpPost]
        public IActionResult GetAllStudents()
        {

            

            var pageLength = int.Parse(Request.Form["length"]);
            var skiped = int.Parse(Request.Form["start"]);
            var searchData = Request.Form["search[value]"];
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"],"][name]")];
            var sortDir = Request.Form["order[0][dir]"];

            var students = studentService.GetAllStudents(searchData[0]);
            var recordsTotal = students.Count();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDir) ))
                students = students.OrderBy(string.Concat(sortColumn , " " , sortDir));


            var data = students.Skip(skiped).Take(pageLength).ToList();

            var studentsViewModel = new List<StudentViewModel>();

            if(students != null)
                foreach (var item in data)
                {
                    var studentViewModel = new StudentViewModel()
                    {
                        Id = item.Id ,
                        FullName = item.FirstName,
                        Identity = item.Identity,
                        Level = Enum.GetName(item.Level.GetType(), item.Level),
                        PhoneNumber  = item.PhoneNumber
                    };
                    studentsViewModel.Add(studentViewModel);
                }
            
            var jsonData = new {recordsFiltered =  recordsTotal , recordsTotal , data = studentsViewModel};

            return Ok(jsonData);
        }

        public void sendMassage(string email)
        {
            Console.WriteLine($"Email : {email} . is Sent");
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View(studentService.GetStudent(id));
        }
        [Authorize(Roles = "admin")]
        // GET: StudentsController/Create
        public ActionResult Create()
        
        {
            ViewData["Enum"] = Enum.GetValues(typeof(AcademicLevel)).Cast<AcademicLevel>()
                                .Select(v =>  new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString()
                                }).ToList();
            return View();
        }
        [Authorize(Roles ="admin")]
        // POST: StudentsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(IFormFile ImageStd , IFormFile ImageId, StudentDto studentDto)
        {
            ViewData["Enum"] = Enum.GetValues(typeof(AcademicLevel)).Cast<AcademicLevel>()
                                .Select(v => new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString()
                                }).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    if (studentDto != null)
                    {
                        var appUserDto = new AppUserDto()
                        {
                            Email = studentDto.Email,
                            PhoneNumber = studentDto.PhoneNumber,
                            UserName = studentDto.Email,
                            PassWord = "user_123_User",
                            ConfirmPassWord = "user_123_User",
                        };
                        var userResult = await appUserService.CreateAppUser(appUserDto , "student");
                        if (!string.IsNullOrWhiteSpace(userResult))
                        {
                            var stdName = await UploadFile(ImageStd);
                            var idName = await UploadFile(ImageId);
                            if (string.IsNullOrEmpty(stdName) || string.IsNullOrWhiteSpace(idName))
                                return View();
                            //var student = mapper.Map<Student>(studentDto);
                            
                            studentDto.ImageStudent = stdName;
                            studentDto.ImageIdentity = idName;
                            studentService.CreateStudent(studentDto , userResult);
                        }
                        else
                        {
                            return View();
                        }
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    return Ok($"Error : {ex}");
                }
            }
            return View();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private async Task<string?> UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(ufile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return fileName;
            }
            return null;
        }
        [Authorize(Roles = "admin,student")]

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Enum"] = Enum.GetValues(typeof(AcademicLevel)).Cast<AcademicLevel>()
                                .Select(v => new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString()
                                }).ToList();
            return View(studentService.GetStudent(id));
        }
        [Authorize(Roles = "admin,student")]
        // POST: StudentsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditAsync(IFormFile ImageStd,IFormFile ImageId ,int id, Student student)
        {
            ViewData["Enum"] = Enum.GetValues(typeof(AcademicLevel)).Cast<AcademicLevel>()
                                .Select(v => new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString()
                                }).ToList();
            try
            {
                if(student != null )
                {
                    if (id != student.Id)
                        return NotFound();
                    var stdName = await UploadFile(ImageStd);
                    var idName = await UploadFile(ImageId);
                    if (!string.IsNullOrWhiteSpace(stdName) && !string.IsNullOrWhiteSpace(idName))
                    {
                        student.ImageStudent = stdName;
                        student.ImageStudent = idName;
                    }
                    var obj = studentService.UpdateStudent(student);
                    if (obj <=0)
                        return View(studentService.GetStudent(id));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(studentService.GetStudent(id));
            }
        }
        [Authorize(Roles = "admin")]
        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(studentService.GetStudent(id));
        }
        [Authorize(Roles = "admin")]
        // POST: StudentsController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id,string appUserId, Student student)
        {
            try
            {
                if (student != null)
                {
                    if (id != student.Id)
                        return NotFound();
                    var obj = studentService.DeleteStudent(id);
                    if (obj == null)
                        return View(studentService.GetStudent(id));
                    if (string.IsNullOrWhiteSpace(appUserId)) return NotFound();
                    await appUserService.DeleteAppUser(appUserId);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(studentService.GetStudent(id));
            }
        }
    }
}
