﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebViewer.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
        public int? HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>()
                .HasMany(e => e.Operators)
                .WithOptional(e => e.Hospital)
                .HasForeignKey(e => e.HospitalId);

            modelBuilder.Entity<Hospital>()
                .HasMany(e => e.Deptments)
                .WithOptional(e => e.Hospital)
                .HasForeignKey(e => e.HospitalId);

            modelBuilder.Entity<Hospital>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Hosptial)
                .HasForeignKey(e => e.HospitalId);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.PacsReports)
                .WithOptional(e => e.Patient)
                .HasForeignKey(e => e.PatientId);

            modelBuilder.Entity<PacsType>()
                .HasMany(e => e.PacsReports)
                .WithOptional(e => e.PacsType)
                .HasForeignKey(e => e.PacsTypeId);

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Deptment> Deptments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PacsReport> PacsReports { get; set; }
        public override IDbSet<ApplicationUser> Users
        {
            get
            {
               
                return base.Users;
            }

            set
            {
                base.Users = value;
            }
        }

        //public System.Data.Entity.DbSet<WebViewer.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}