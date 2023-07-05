using AcademySystem.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.EF
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>()
                .Property(b => b.TimeOfRegistration)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Student>()
                .HasOne(b => b.appUser)
                .WithOne(c => c.student)
                .HasForeignKey<Student>(b => b.appUserId);

            builder.Entity<Loging>()
                .HasOne(b => b.appUser)
                .WithOne(c => c.loging)
                .HasForeignKey<Loging>(b => b.appUserId);

            var idUser = Guid.NewGuid().ToString();
            var idRole = Guid.NewGuid().ToString();
            builder.Entity<AppUser>()
                .HasData(new AppUser
                {
                    Id = idUser,
                    Email = "Admin@admin",
                    PhoneNumber = "0595195186",
                    UserName = "Admin@admin",
                    PasswordHash = "AQAAAAEAACcQAAAAED3EhZpief2srOsE6dbRM46UJ8fDiKLX5TuyuLO9WafYZ1nPgvDpqg//t/iV3E38zA==",
                    LockoutEnabled = true,
                    NormalizedEmail = "ADMIN@ADMIN",
                    NormalizedUserName = "ADMIN@ADMIN",

                    
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = idRole,
                    Name = "admin",
                });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = idRole,
                    UserId = idUser,
                });
            builder.Entity<Loging>()
                .HasData(new Loging 
                {
                 appUserId = idUser,
                 IsLogging = false
                });
        }

        

        public DbSet<Student>? Students { get; set; }
        public DbSet<Loging> Logings { get; set; }
    }
}
