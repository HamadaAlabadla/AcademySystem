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

        public string? CreateLoging(Loging loging)
        {
            if(dBContext.Add(loging) ==null) return null;
            dBContext.SaveChanges();
            return loging.appUserId;
        }

        public Loging? DeleteLoging(string appUserId)
        {
            if(string.IsNullOrWhiteSpace(appUserId)) return null;
            var loging = GetByappUserId(appUserId);
            if(loging == null) return null;
            dBContext.Remove(loging);
            dBContext.SaveChanges();
            return loging;
        }

        public List<Loging>? GetAllLogings(List<AppUser> data)
        {
            return dBContext.Logings.Where(x => data.Where(b => b.Id.Equals(x.appUserId)).Any()).ToList();
        }

        public Loging? GetByappUserId(string appUserId)
        {
            if (string.IsNullOrWhiteSpace(appUserId)) return null;
            return dBContext.Logings.FirstOrDefault( x => x.appUserId == appUserId );
        }

        public string? UpdateLoging(Loging? loging)
        {
            if(loging == null || string.IsNullOrWhiteSpace(loging.appUserId)) return null;
            loging = GetByappUserId(loging.appUserId);
            if(loging == null ) return null;
            dBContext.Update(loging);
            dBContext.SaveChanges();
            return loging.appUserId;
        }
    }
}
