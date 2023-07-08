using AcademySystem.Core.Dtos;
using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using AcademySystem.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AcademySystem.Web.Controllers
{
    [Authorize]
    public class AppUsersController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly ILogingInterface logingInterface;

        public AppUsersController(
            IAppUserService appUserService,
            ILogingInterface logingInterface)
        {
            this.appUserService = appUserService;
            this.logingInterface = logingInterface;
        }


        // GET: AppUsersController
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetAllUsers()
        {
            var pageLength = int.Parse(Request.Form["length"]);
            var skiped = int.Parse(Request.Form["start"]);

            var appUsers = appUserService.GetAllAppUsers();
            var appUsersCount = appUsers.Count();

            var data = appUsers.Skip(skiped).Take(pageLength).ToList();


           // var logings = logingInterface.GetAllLogings(data);
            //var appUsersViewModels = new List<AppUserViewModel>();
            //if(data != null )
            //    foreach (var item in data)
            //    {
            //        var appUserViewModel = new AppUserViewModel()
            //        {
            //            Email = item.Email,
            //            PhoneNumber = item.PhoneNumber,
            //            UserName = item.UserName,
            //            isActive = (logings.FirstOrDefault(x => x.appUserId!.Equals(item.Id)) == null) ? false : logings.First(x => x.appUserId!.Equals(item.Id)).IsLogging,
            //        };
            //        appUsersViewModels.Add(appUserViewModel);
            //    }
            var json = new { recordsFiltered = appUsersCount, appUsersCount, data = appUsers };
            return Ok(json);
        }

        [Authorize(Roles = "admin")]
        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: AppUsersController/Create
        [HttpPost]
        public async Task<ActionResult> Create(AppUserDto appUserDto)
        {
            try
            {
                
                if (appUserDto == null || !appUserDto.PassWord!.Equals(appUserDto.ConfirmPassWord))
                    return View();

                var result = await appUserService.CreateAppUser(appUserDto, "user");

                if (!string.IsNullOrWhiteSpace(result))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(appUserDto);
            }
            catch
            {
                return View(appUserDto);
            }
        }
        [Authorize(Roles = "admin,student")]
        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await appUserService.GetAppUser(id);
            if (user == null) return NotFound();
            return View( new AppUserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            });
        }
        [Authorize(Roles = "admin,student")]
        // POST: AppUsersController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, AppUserDto appUserDto)
        {
            try
            {
                if (appUserDto == null || !appUserDto.Id!.Equals(id))
                    return View();
                var user = await appUserService.GetAppUser(id);
                if (user == null) return NotFound();
                user.Email = appUserDto.Email;
                user.PhoneNumber = appUserDto.PhoneNumber;
                user.UserName = appUserDto.UserName;

                await appUserService.UpdateAppUser(user);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(appUserDto);
            }
        }
        [Authorize(Roles = "admin")]
        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            return View(await appUserService.DeleteAppUser(id));
        }
        [Authorize(Roles = "admin")]
        // POST: AppUsersController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, AppUser appUser)
        {
            var user = await appUserService.GetAppUser(id);
            if (user == null) return NotFound();
            try
            {
                
                await appUserService.DeleteAppUser(user.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(user);
            }
        }
    }
}
