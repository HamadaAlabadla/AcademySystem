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

namespace Students.Web.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IBaseRepository<Student> baseRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        //private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogingInterface logingRepository;
        private readonly IBaseRepository<Loging> logingBaseRepository;

        public StudentsController(IBaseRepository<Student> baseRepository,
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
           // IUserEmailStore<AppUser> emailStore,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            ILogingInterface logingRepository,
            IBaseRepository<Loging> logingBaseRepository)
        {
            this.baseRepository = baseRepository;
            _userManager = userManager;
            _userStore = userStore;
            //_emailStore = emailStore;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.logingRepository = logingRepository;
            this.logingBaseRepository = logingBaseRepository;
        }

        public async Task<ActionResult> Index()
        {
            return View(await baseRepository.GetAllAsync());
        }
        // GET: StudentsController
        public ActionResult GetById()
        {
            return View(baseRepository.GetById(1001));
        }

        // GET: StudentsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await baseRepository.GetByIdAsync(id));
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
        //[ValidateAntiForgeryToken]
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
                        var user = CreateUser();
                        await _userStore.SetUserNameAsync(user, studentDto.Email, CancellationToken.None);
                       // await _emailStore.SetEmailAsync(user, studentDto.Email, CancellationToken.None);
                        var result = await _userManager.CreateAsync(user, "user_123_USER");

                        if (result.Succeeded)
                        {
                            var role = await roleManager.RoleExistsAsync("student");
                            if (!role)
                                await roleManager.CreateAsync(new IdentityRole { Name = "student" });
                            var Loging = new Loging() {
                                appUser = user,
                                appUserId = user.Id,
                                IsLogging = false,
                            };
                            logingBaseRepository.Create(Loging);
                            var userId = await _userManager.GetUserIdAsync(user);
                            user = await _userManager.FindByIdAsync(userId);
                            var roleResult = await _userManager.AddToRoleAsync(user, "student");
                            var stdName = await UploadFile(ImageStd);
                            var idName = await UploadFile(ImageId);
                            if (string.IsNullOrEmpty(stdName) || string.IsNullOrWhiteSpace(idName))
                                return View();
                            //var student = mapper.Map<Student>(studentDto);
                            var student = new Student()
                            {
                                Id = studentDto.Id,
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
                            student.ImageStudent = stdName;
                            student.ImageIdentity = idName;
                            student.appUserId = userId;
                            var obj = baseRepository.Create(student);
                            if (obj == null)
                                return View();
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);

                            }
                            return View();
                        }
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
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
        [Authorize(Roles = "admin")]

        // GET: StudentsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewData["Enum"] = Enum.GetValues(typeof(AcademicLevel)).Cast<AcademicLevel>()
                                .Select(v => new SelectListItem
                                {
                                    Text = v.ToString(),
                                    Value = ((int)v).ToString()
                                }).ToList();
            return View(await baseRepository.GetByIdAsync(id));
        }
        [Authorize(Roles = "admin")]
        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    if (!string.IsNullOrEmpty(stdName) && !string.IsNullOrWhiteSpace(idName))
                    {
                        student.ImageStudent = stdName;
                        student.ImageStudent = idName;
                    }
                    var obj = baseRepository.Update(student);
                    if (obj == null)
                        return View(baseRepository.GetByIdAsync(id));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(baseRepository.GetByIdAsync(id));
            }
        }

        // GET: StudentsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await baseRepository.GetByIdAsync(id));
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id,string appUserId, Student student)
        {
            try
            {
                if (student != null)
                {
                    if (id != student.Id)
                        return NotFound();
                    var obj = baseRepository.Delete(id);
                    if (obj == null)
                        return View(baseRepository.GetByIdAsync(id));
                    await _userManager.DeleteAsync(await _userManager.FindByIdAsync(appUserId));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(baseRepository.GetByIdAsync(id));
            }
        }
    }
}
