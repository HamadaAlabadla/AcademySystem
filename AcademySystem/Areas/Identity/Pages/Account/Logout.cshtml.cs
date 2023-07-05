﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using AcademySystem.EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AcademySystem.Web.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogingInterface logingRepository;
        private readonly IBaseRepository<Loging> logingBaseRepository;

        public LogoutModel(
            SignInManager<AppUser> signInManager,
            ILogger<LogoutModel> logger,
            UserManager<AppUser> userManager,
            ILogingInterface logingRepository,
            IBaseRepository<Loging> logingBaseRepository)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            this.logingRepository = logingRepository;
            this.logingBaseRepository = logingBaseRepository;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var loging = logingRepository.GetByappUserId(user.Id);
            //loging.IsLogging = false;
            //logingBaseRepository.Update(loging);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}