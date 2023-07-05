using AcademySystem.Core.Dtos;
using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AcademySystem.Web.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IBaseRepository<Loging> baseRepository;

        public AppUsersController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IBaseRepository<Loging> baseRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.baseRepository = baseRepository;
        }


        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            var Logings = await baseRepository.GetAllAsync();
            ViewData["Logings"] = Logings;
            return View(userManager.Users.ToList());
        }


        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppUserDto appUserDto)
        {
            var user = CreateUser();
            try
            {
                
                if (appUserDto == null || !appUserDto.PassWord!.Equals(appUserDto.ConfirmPassWord))
                    return View();
                
                user = new AppUser()
                {
                    UserName = appUserDto.UserName,
                    Email = appUserDto.Email,
                    PhoneNumber = appUserDto.PhoneNumber,
                };
                // await _emailStore.SetEmailAsync(user, studentDto.Email, CancellationToken.None);
                var result = await userManager.CreateAsync(user, appUserDto.PassWord);

                if (result.Succeeded)
                {
                    var role = await roleManager.RoleExistsAsync("user");
                    if (!role)
                        await roleManager.CreateAsync(new IdentityRole { Name = "user" });
                    await userManager.AddToRoleAsync(user, "user");

                    var Loging = new Loging()
                    {
                        appUser = user,
                        appUserId = user.Id,
                        IsLogging = false,
                    };
                    baseRepository.Create(Loging);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View( new AppUserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            });
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, AppUserDto appUserDto)
        {
            try
            {
                if (appUserDto == null || !appUserDto.Id!.Equals(id))
                    return View();
                var user = await userManager.FindByIdAsync(id);
                user.Email = appUserDto.Email;
                user.PhoneNumber = appUserDto.PhoneNumber;
                user.UserName = appUserDto.UserName;

                await userManager.UpdateAsync(user);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(appUserDto);
            }
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            return View(await userManager.FindByIdAsync(id));
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, AppUser appUser)
        {
            var user = await userManager.FindByIdAsync(id);
            try
            {
                
                await userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(user);
            }
        }
    }
}
