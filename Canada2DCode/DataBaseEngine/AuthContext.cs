using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;

namespace Canada2DCode.DataBaseEngine
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

        //    modelBuilder.Entity<ApplicationUser>().ToTable("User");
        //    modelBuilder.Entity<IdentityRole>().ToTable("Role");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        //}

        //public static AuthContext Create()
        //{
        //    return new AuthContext();
        //}

    }
}