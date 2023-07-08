using AcademySystem.Core.Dtos;
using AcademySystem.Core.Models;
using AcademySystem.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUser?> GetAppUser(string id);
        IQueryable<AppUserViewModel> GetAllAppUsers();
        Task<string?> CreateAppUser(AppUserDto appUserDto, string role);
        Task<string?> UpdateAppUser(AppUser? appUser);
        Task<AppUser?> DeleteAppUser(string id);
    }
}
