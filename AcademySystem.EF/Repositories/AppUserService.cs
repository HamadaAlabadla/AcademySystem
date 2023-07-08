using AcademySystem.Core.Dtos;
using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using AcademySystem.Core.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.EF.Repositories
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly ILogingInterface _logger;
        private readonly ApplicationDbContext dBcontext;

        public AppUserService(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<AppUser> userStore,
            ILogingInterface logger,
            ApplicationDbContext dBcontext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _userStore = userStore;
            _logger = logger;
            this.dBcontext = dBcontext;
        }

        public async Task<string?> CreateAppUser(AppUserDto appUserDto, string role)
        {
            if (appUserDto == null
                || string.IsNullOrWhiteSpace(appUserDto.PassWord)
                || string.IsNullOrWhiteSpace(appUserDto.ConfirmPassWord)
                || string.IsNullOrWhiteSpace(role))
                return null;
            var appUser = CreateUser();
            if (appUser == null || !appUserDto.PassWord.Equals(appUserDto.ConfirmPassWord)) return null;
            await _userStore.SetUserNameAsync(appUser, appUserDto.Email, CancellationToken.None);
            var result = await userManager.CreateAsync(appUser, appUserDto.PassWord);

            if (result.Succeeded)
            {
                var isExsit = await roleManager.RoleExistsAsync(role);
                if (!isExsit)
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                await userManager.AddToRoleAsync(appUser, role);
                _logger.CreateLoging(new Loging() { appUser = appUser ,appUserId = appUser.Id , IsLogging = false});
                return appUser.Id;
            }
            return null;
        }

        private AppUser? CreateUser()
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

        public async Task<AppUser?> DeleteAppUser(string id)
        {
            var appUser = await GetAppUser(id);
            if (appUser == null) return null;
            await userManager.DeleteAsync(appUser);
            return appUser;
        }

        public IQueryable<AppUserViewModel> GetAllAppUsers()
        {
            return userManager.Users
                .Join(
                    dBcontext.Logings,
                    appUser => appUser.Id,
                    loging => loging.appUserId,
                    (appUser, loging) => new AppUserViewModel
                    {
                        Email = appUser.Email,
                        PhoneNumber = appUser.PhoneNumber,
                        UserName = appUser.UserName,
                        isActive = loging.IsLogging
                    }
                );
        }

        public async Task<AppUser?> GetAppUser(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<string?> UpdateAppUser(AppUser? appUser)
        {
            if (appUser == null) return null;
            appUser = await GetAppUser(appUser.Id);
            if (appUser == null) return null;
            await userManager.UpdateAsync(appUser);
            return appUser.Id;
        }

    }
}
