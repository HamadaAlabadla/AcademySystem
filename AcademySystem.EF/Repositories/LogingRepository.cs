using AcademySystem.Core.Interfaces;
using AcademySystem.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.EF.Repositories
{
    public class LogingRepository :ILogingInterface
    {
        private readonly ApplicationDbContext dBContext;
        public LogingRepository(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Loging? GetByappUserId(string appUserId)
        {

            return dBContext.Logings.FirstOrDefault( x => x.appUserId == appUserId );
        }
    }
}
