using BusinessCenter.Identity.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Identity
{
    public partial class ApplicationDbContext : IdentityDbContext<AppUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region constructors and destructors

        public ApplicationDbContext()
            : base("IdentityConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.UseDatabaseNullSemantics = true;
            var objectContextAdapter = this as IObjectContextAdapter;
            objectContextAdapter.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior = true;
        }

        #endregion constructors and destructors

        #region methods

        /// <summary>
        /// Creates a New ApplicationDbContext
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// This Method is Used to Map Table Names in the Database
        /// with the Entites in the EntityFramework and also
        /// Maps the Column name(s) of a Table Column(s) with the Entity Properties
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Map Entities to their tables.
            modelBuilder.Entity<AppUser>().ToTable("User");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole");

            modelBuilder.Entity<AppUser>().Property(r => r.PasswordHash).HasColumnName("Password").HasMaxLength(1024);
            modelBuilder.Entity<AppUser>().Property(r => r.Address).HasColumnName("Address").HasMaxLength(500);
            modelBuilder.Entity<AppUser>().Property(r => r.City).HasColumnName("City").HasMaxLength(500);
            modelBuilder.Entity<AppUser>().Property(r => r.State).HasColumnName("State").HasMaxLength(500);

            modelBuilder.Entity<AppUser>().Property(r => r.FirstName).HasColumnName("FirstName").HasMaxLength(200);
            modelBuilder.Entity<AppUser>().Property(r => r.LastName).HasColumnName("LastName").HasMaxLength(200);

            modelBuilder.Entity<AppUser>().Property(r => r.MobileNumber).HasColumnName("MobileNumber").HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(r => r.PostalCode).HasColumnName("PostalCode").HasMaxLength(100);

            modelBuilder.Entity<AppUser>().Property(r => r.ActivationCode).HasColumnName("ActivationCode").HasMaxLength(1024);
            modelBuilder.Entity<AppUser>().Property(r => r.SecurityStamp).HasColumnName("SecurityStamp").HasMaxLength(1024);

            modelBuilder.Entity<AppUser>().Property(r => r.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(100);

            modelBuilder.Entity<ApplicationUserClaim>().Property(r => r.ClaimType).HasColumnName("ClaimType").HasMaxLength(1024);
            modelBuilder.Entity<ApplicationUserClaim>().Property(r => r.ClaimValue).HasColumnName("ClaimValue").HasMaxLength(1024);

            modelBuilder.Entity<AppUser>().Property(u => u.UserName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
            modelBuilder.Entity<AppUser>().Property(u => u.Email).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
        }

        #endregion methods
    }
}